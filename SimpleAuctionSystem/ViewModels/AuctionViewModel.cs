using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleAuctionSystem.ViewModels
{
    public class AuctionViewModel
    {
        public IEnumerable<ParticipantViewModel> participants { set; get; }

        public IEnumerable<ItemViewModel> items { set; get; }

        [Required(ErrorMessage = "Participant Required Field !!")]
        [RegularExpression(@"^[0-9]{1,11}(?:\.[0-9]{1,8})?$", ErrorMessage = "The Bid must be numbers with 8 digits after point only !!")]
        public decimal? Bid { set; get; }

        [Required(ErrorMessage = "Participant Required Field !!")]
        public decimal? ParticipantId { set; get; }

        [Required(ErrorMessage = "Item Required Field !!")]
        public decimal? ItemId { set; get; }

        public decimal? Profit { set; get; }

        public string ParticipantName { set; get; }

    }
}