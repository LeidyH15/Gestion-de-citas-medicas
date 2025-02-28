using MicroservicePersonas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MicroservicePersonas.Infraestructure.Persistence
{
	public class PersonasDbContext : DbContext
    {
        public PersonasDbContext() : base("name=PersonasDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<TipoPersona> TipoPersonas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().
                ToTable("Persona");
            modelBuilder.Entity<TipoPersona>()
                .ToTable("TipoPersona");
            base.OnModelCreating(modelBuilder);
        }
    }
}