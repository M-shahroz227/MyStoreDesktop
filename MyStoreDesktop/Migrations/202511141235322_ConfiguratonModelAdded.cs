namespace MyStoreDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfiguratonModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        ConfigId = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConfigId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Configurations");
        }
    }
}
