namespace HomeMultimediaLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotNullFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Publisher", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Summary", c => c.String(nullable: false));
            AlterColumn("dbo.ReadingItems", "ISBN", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReadingItems", "ISBN", c => c.String());
            AlterColumn("dbo.Items", "Summary", c => c.String());
            AlterColumn("dbo.Items", "Publisher", c => c.String());
            AlterColumn("dbo.Items", "Author", c => c.String());
            AlterColumn("dbo.Items", "Name", c => c.String());
        }
    }
}
