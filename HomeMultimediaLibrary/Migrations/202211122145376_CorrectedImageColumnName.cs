namespace HomeMultimediaLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedImageColumnName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Items", name: "ImageId_Id", newName: "Image_Id");
            RenameIndex(table: "dbo.Items", name: "IX_ImageId_Id", newName: "IX_Image_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Items", name: "IX_Image_Id", newName: "IX_ImageId_Id");
            RenameColumn(table: "dbo.Items", name: "Image_Id", newName: "ImageId_Id");
        }
    }
}
