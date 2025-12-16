namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDriveEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductImageSettings", "DriveLabel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductImageSettings", "DriveLabel", c => c.String(maxLength: 50));
        }
    }
}
