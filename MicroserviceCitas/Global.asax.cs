using AutoMapper;
using MicroserviceCitas.Application.Interfaces;
using MicroserviceCitas.Application.Mappings;
using MicroserviceCitas.Application.Services;
using MicroserviceCitas.Domain.Interfaces;
using MicroserviceCitas.Infraestructure.Persistence;
using MicroserviceCitas.Infraestructure.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Linq;
using System.Reflection;
using System;
using System.Web.Http;
using Container = SimpleInjector.Container;
using MediatR;

namespace MicroserviceCitas
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Registrar AutoMapper
            RegisterAutoMapper(container);

            // Registro de dependencias
            RegisterDependencies(container);

            // Registrar MediatR
            RegisterMediatR(container);

            // Registrar controladores
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            // Verificar la configuración
            container.Verify();

            // Configurar Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void RegisterDependencies(Container container)
        {
            container.Register<CitasContext>(Lifestyle.Scoped);
            container.Register<ICitaService, CitaService>(Lifestyle.Scoped);
            container.Register<ICitaRepository, CitaRepository>(Lifestyle.Scoped);
            container.Register<IPersonaService, PersonaService>(Lifestyle.Scoped);
            container.Register<IRabbitMQSender, RabbitMQSender>(Lifestyle.Scoped);
        }

        private void RegisterAutoMapper(Container container)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CitaProfile>();
            });

            container.RegisterInstance(config.CreateMapper());
        }

        private void RegisterMediatR(Container container)
        {
            if (!container.GetCurrentRegistrations().Any(r => r.ServiceType == typeof(IMediator)))
            {
                container.RegisterSingleton<IMediator>(() => new Mediator(container.GetInstance<ServiceFactory>()));
            }

            var assemblies = new[]
            {
        Assembly.GetExecutingAssembly(),
        typeof(MicroserviceCitas.Application.Queries.GetCitaByIdQuery).Assembly };

            container.Collection.Register(typeof(IPipelineBehavior<,>), Enumerable.Empty<Type>());

            // Registrar MediatR como Singleton
            //container.RegisterSingleton<IMediator, Mediator>();

            // Registrar los manejadores de solicitudes (queries y comandos)
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);

            // Registrar ServiceFactory correctamente
            container.RegisterInstance<ServiceFactory>(type => container.GetInstance(type));

            // Registrar Mediator con su ServiceFactory
            //container.RegisterSingleton<IMediator>(() => new Mediator(container.GetInstance<ServiceFactory>()));
        }
    }
}
