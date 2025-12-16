namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductImageSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImageSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImagePath = c.String(maxLength: 500),
                        DriveLabel = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImageSettings", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductImageSettings", new[] { "ProductId" });
            DropTable("dbo.ProductImageSettings");
        }
    }
}
