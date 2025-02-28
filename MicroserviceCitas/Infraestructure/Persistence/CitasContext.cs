using MicroserviceCitas.Domain.Entities;
using System.Data.Entity;

namespace MicroserviceCitas.Infraestructure.Persistence
{
    public class CitasContext : DbContext
    {
        public CitasContext() : base("name=CitasDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>().
                ToTable("Cita");

            base.OnModelCreating(modelBuilder);
        }
    }
}