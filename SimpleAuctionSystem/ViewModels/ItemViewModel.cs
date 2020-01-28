using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleAuctionSystem.ViewModels
{
    public class ItemViewModel
    {
        public decimal ItemId { set; get; }

        [Required(ErrorMessage ="Item Name Required Field !!")]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessage = "The Item name must be letters and numbers only !!")]
        public string ItemName { set; get; }

        [Required(ErrorMessage = "Item Name Required Field !!")]
        [RegularExpression(@"^[0-9]{1,11}(?:\.[0-9]{1,8})?$", ErrorMessage = "The Item Start price must be numbers with 8 digits after point only !!")]
        public decimal? ItemStartPrice { set; get; }


        public IEnumerable<ItemViewModel> itemsList { set; get; } = new List<ItemViewModel>();
    }
}