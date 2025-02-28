namespace MicroservicePersonas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivoToPersona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persona", "Activo", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Persona", "Genero", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Persona", "Genero", c => c.String());
            DropColumn("dbo.Persona", "Activo");
        }
    }
}
