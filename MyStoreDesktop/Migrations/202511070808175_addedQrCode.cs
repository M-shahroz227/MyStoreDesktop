namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedQrCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "QrCode", c => c.String(maxLength: 255));
            DropColumn("dbo.Products", "BarCodeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "BarCodeId", c => c.String(maxLength: 50));
            DropColumn("dbo.Products", "QrCode");
        }
    }
}
