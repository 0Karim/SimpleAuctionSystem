using SimpleAuctionSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleAuctionSystem.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("AuctionSystemConnString") { }

        public virtual DbSet<Items> Items { set; get; }

        public virtual DbSet<Participants> Participants { set; get; }

        public virtual DbSet<AuctionProcess> AuctionProcesses { set; get; }
    }
}