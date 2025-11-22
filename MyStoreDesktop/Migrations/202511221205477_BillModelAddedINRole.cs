namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BillModelAddedINRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bills", "Role");
        }
    }
}
