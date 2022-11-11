namespace HomeMultimediaLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Issuer = c.String(),
                        Summary = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        AddedById_Id = c.String(maxLength: 128),
                        ImageId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById_Id)
                .ForeignKey("dbo.Images", t => t.ImageId_Id)
                .Index(t => t.AddedById_Id)
                .Index(t => t.ImageId_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Base64 = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MultimediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LengthMinutes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AlbumItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MultimediaItems", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ReadingItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Pages = c.Int(nullable: false),
                        ISBN = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.BookItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReadingItems", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.FilmItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MultimediaItems", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MagazineItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReadingItems", t => t.Id)
                .Index(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MagazineItems", "Id", "dbo.ReadingItems");
            DropForeignKey("dbo.FilmItems", "Id", "dbo.MultimediaItems");
            DropForeignKey("dbo.BookItems", "Id", "dbo.ReadingItems");
            DropForeignKey("dbo.ReadingItems", "Id", "dbo.Items");
            DropForeignKey("dbo.AlbumItems", "Id", "dbo.MultimediaItems");
            DropForeignKey("dbo.MultimediaItems", "Id", "dbo.Items");
            DropForeignKey("dbo.Items", "ImageId_Id", "dbo.Images");
            DropForeignKey("dbo.Items", "AddedById_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MagazineItems", new[] { "Id" });
            DropIndex("dbo.FilmItems", new[] { "Id" });
            DropIndex("dbo.BookItems", new[] { "Id" });
            DropIndex("dbo.ReadingItems", new[] { "Id" });
            DropIndex("dbo.AlbumItems", new[] { "Id" });
            DropIndex("dbo.MultimediaItems", new[] { "Id" });
            DropIndex("dbo.Items", new[] { "ImageId_Id" });
            DropIndex("dbo.Items", new[] { "AddedById_Id" });
            DropTable("dbo.MagazineItems");
            DropTable("dbo.FilmItems");
            DropTable("dbo.BookItems");
            DropTable("dbo.ReadingItems");
            DropTable("dbo.AlbumItems");
            DropTable("dbo.MultimediaItems");
            DropTable("dbo.Images");
            DropTable("dbo.Items");
        }
    }
}
