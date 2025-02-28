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
using System.ComponentModel;
using System.Configuration;
using System.Web.Http;
using Container = SimpleInjector.Container;

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
    }
}
