namespace HomeMultimediaLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameItemForeignKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Items", name: "AddedById_Id", newName: "AddedByUserId");
            RenameIndex(table: "dbo.Items", name: "IX_AddedById_Id", newName: "IX_AddedByUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Items", name: "IX_AddedByUserId", newName: "IX_AddedById_Id");
            RenameColumn(table: "dbo.Items", name: "AddedByUserId", newName: "AddedById_Id");
        }
    }
}
