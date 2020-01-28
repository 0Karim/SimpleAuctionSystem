namespace SimpleAuctionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnIsWinner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuctionProcesses", "isWinner", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuctionProcesses", "isWinner");
        }
    }
}
