using AutoMapper;
using MediatR;
using MicroservicePersonas.Application.Mappings;
using MicroservicePersonas.Application.Interfaces;
using MicroservicePersonas.Application.Services;
using MicroservicePersonas.Domain.Interfaces;
using MicroservicePersonas.Infraestructure.Persistence;
using MicroservicePersonas.Infraestructure.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Reflection;
using System.Web.Http;
using System.Linq;
using System;

namespace MicroservicePersonas
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Registrar AutoMapper
            RegisterAutoMapper(container);

            // Registrar MediatR
            RegisterMediatR(container);

            // Registro de dependencias
            RegisterDependencies(container);

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
            container.Register<PersonasDbContext>(Lifestyle.Scoped);
            container.Register<IPersonaService, PersonaService>(Lifestyle.Scoped);
            container.Register<IPersonaRepository, PersonaRepository>(Lifestyle.Scoped);
        }

        private void RegisterAutoMapper(Container container)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PersonaProfile>();
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
        typeof(MicroservicePersonas.Application.Queries.GetPersonaByIdQuery).Assembly
    };
            
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
