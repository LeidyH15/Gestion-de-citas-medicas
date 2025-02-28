namespace MicroservicePersonas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Persona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        IdTipoPersona = c.Int(nullable: false),
                        Identificacion = c.String(maxLength: 20),
                        Fecha_Nacimiento = c.DateTime(nullable: false, storeType: "date"),
                        Telefono = c.String(),
                        Email = c.String(),
                        Especialidad = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoPersona", t => t.IdTipoPersona, cascadeDelete: true)
                .Index(t => t.IdTipoPersona)
                .Index(t => t.Identificacion, unique: true);
            
            CreateTable(
                "dbo.TipoPersona",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Descriptor = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Persona", "IdTipoPersona", "dbo.TipoPersona");
            DropIndex("dbo.Persona", new[] { "Identificacion" });
            DropIndex("dbo.Persona", new[] { "IdTipoPersona" });
            DropTable("dbo.TipoPersona");
            DropTable("dbo.Persona");
        }
    }
}
