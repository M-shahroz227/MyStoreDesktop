namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesSomeFeatures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "itemPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Bills", "SalePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "SalePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Bills", "itemPrice");
        }
    }
}
