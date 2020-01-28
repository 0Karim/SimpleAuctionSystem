using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleAuctionSystem.Models
{
    public class AuctionProcess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int? ParticipantId { set; get; }

        public int? ItemId { set; get; }

        public decimal? Bid { set; get; }

        public decimal? Profit { set; get; }

        public bool? isWinner { set; get; }

        public virtual  ICollection<Participants> Participants { get; set; }
        public virtual Items Item { get; set; }
    }
}