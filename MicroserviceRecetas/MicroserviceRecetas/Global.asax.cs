using AutoMapper;
using MediatR;
using MicroserviceRecetas.Application.Interfaces;
using MicroserviceRecetas.Application.Mappings;
using MicroserviceRecetas.Application.Services;
using MicroserviceRecetas.Domain.Interfaces;
using MicroserviceRecetas.Infraestructure.Percistence;
using MicroserviceRecetas.Infraestructure.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Linq;
using System.Reflection;
using System;
using System.Threading;
using System.Web.Http;
using Container = SimpleInjector.Container;

namespace MicroserviceRecetas
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

            StartRabbitMQListener(container);
        }

        private void RegisterDependencies(Container container)
        {
            container.Register<RecetasContext>(Lifestyle.Scoped);
            container.Register<IRecetaService, RecetaService>(Lifestyle.Scoped);
            container.Register<IRecetaRepository, RecetaRepository>(Lifestyle.Scoped);
            container.Register<IPersonaService, PersonaService>(Lifestyle.Scoped);
            container.Register<ICitaService, CitaService>(Lifestyle.Scoped);
        }

        private void RegisterAutoMapper(Container container)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RecetaProfile>();
            });

            container.RegisterInstance(config.CreateMapper());
        }

        private void StartRabbitMQListener(Container container)
        {

            // Inicia el listener en un hilo separado
            Thread listenerThread = new Thread(() =>
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    var _recetaService = container.GetInstance<IRecetaService>();
                    RabbitMQListener listener = new RabbitMQListener(_recetaService);
                    listener.StartListening();
                }
            });

            listenerThread.IsBackground = true;
            listenerThread.Start();
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
        typeof(MicroserviceRecetas.Application.Queries.GetRecetaByIdQuery).Assembly };

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
