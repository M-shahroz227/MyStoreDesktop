namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductCode", c => c.String(maxLength: 255));
            AddColumn("dbo.Products", "CodeType", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "QrCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "QrCode", c => c.String(maxLength: 255));
            DropColumn("dbo.Products", "CodeType");
            DropColumn("dbo.Products", "ProductCode");
        }
    }
}
