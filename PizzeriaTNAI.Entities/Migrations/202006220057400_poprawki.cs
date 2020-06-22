namespace PizzeriaTNAI.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawki : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        City = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 255),
                        ZipCode = c.String(nullable: false, maxLength: 255),
                        DateOfCreation = c.DateTime(nullable: false),
                        OverallPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.AspNetUsers", "AddressData_Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressData_City", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressData_ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "AddressData_ZipCode");
            DropColumn("dbo.AspNetUsers", "AddressData_City");
            DropColumn("dbo.AspNetUsers", "AddressData_Address");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
        }
    }
}
