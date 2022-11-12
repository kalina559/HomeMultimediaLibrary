namespace HomeMultimediaLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameIssuerToPublisher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Publisher", c => c.String());
            DropColumn("dbo.Items", "Issuer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Issuer", c => c.String());
            DropColumn("dbo.Items", "Publisher");
        }
    }
}
