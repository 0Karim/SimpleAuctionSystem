namespace SimpleAuctionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuctionProcesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParticipantId = c.Int(),
                        ItemId = c.Int(),
                        Bid = c.Decimal(precision: 18, scale: 2),
                        Profit = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        StartPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParticipantName = c.String(),
                        AuctionProcess_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuctionProcesses", t => t.AuctionProcess_Id)
                .Index(t => t.AuctionProcess_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "AuctionProcess_Id", "dbo.AuctionProcesses");
            DropForeignKey("dbo.AuctionProcesses", "ItemId", "dbo.Items");
            DropIndex("dbo.Participants", new[] { "AuctionProcess_Id" });
            DropIndex("dbo.AuctionProcesses", new[] { "ItemId" });
            DropTable("dbo.Participants");
            DropTable("dbo.Items");
            DropTable("dbo.AuctionProcesses");
        }
    }
}
