namespace MicroservicePersonas.Migrations
{
    using MicroservicePersonas.Domain.Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MicroservicePersonas.Infraestructure.Persistence.PersonasDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MicroservicePersonas.Infraestructure.Persistence.PersonasDbContext context)
        {
            context.TipoPersonas.AddOrUpdate(
                tp => tp.Id,
                new TipoPersona { Id = 1, Descriptor = "Médico" },
                new TipoPersona { Id = 2, Descriptor = "Paciente" }
                );

            context.SaveChanges();
        }
    }
}
