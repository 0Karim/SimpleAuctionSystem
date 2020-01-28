using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleAuctionSystem.ViewModels
{
    public class ParticipantViewModel
    {
        public decimal participantId { set; get; }

        [Required(ErrorMessage = "Participant Name Required Field !!")]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessage = "The Participant name must be letters and numbers only !!")]
        public string ParticipantName { set; get; }


        public IEnumerable<ParticipantViewModel> itemsList { set; get; } = new List<ParticipantViewModel>();
    }
}