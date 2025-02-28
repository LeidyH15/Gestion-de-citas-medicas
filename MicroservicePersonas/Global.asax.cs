using AutoMapper;
using MicroservicePersonas.Application.Mappings;
using MicroservicePersonas.Application.Interfaces;
using MicroservicePersonas.Application.Services;
using MicroservicePersonas.Domain.Interfaces;
using MicroservicePersonas.Infraestructure.Persistence;
using MicroservicePersonas.Infraestructure.Repositories;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Container = SimpleInjector.Container;

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
    }
}