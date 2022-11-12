namespace HomeMultimediaLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKeywordsColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Keywords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Keywords");
        }
    }
}
