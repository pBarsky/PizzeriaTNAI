namespace PizzeriaTNAI.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class go : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderItems");
            AddPrimaryKey("dbo.OrderItems", new[] { "OrderId", "ProductId" });
            DropColumn("dbo.OrderItems", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.OrderItems");
            AddPrimaryKey("dbo.OrderItems", "Id");
        }
    }
}
