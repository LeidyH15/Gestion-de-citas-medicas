using MicroserviceRecetas.Domain.Entities;
using System.Data.Entity;

namespace MicroserviceRecetas.Infraestructure.Percistence
{
    public class RecetasContext : DbContext
    {
        public RecetasContext() : base("name=RecetasDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receta>().
                ToTable("Receta");
            modelBuilder.Entity<Medicamento>()
                .ToTable("Medicamento");
            base.OnModelCreating(modelBuilder);
        }
    }
}