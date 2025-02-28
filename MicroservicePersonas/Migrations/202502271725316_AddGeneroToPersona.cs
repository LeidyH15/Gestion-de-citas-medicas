namespace MicroservicePersonas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGeneroToPersona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persona", "Genero", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Persona", "Genero");
        }
    }
}
