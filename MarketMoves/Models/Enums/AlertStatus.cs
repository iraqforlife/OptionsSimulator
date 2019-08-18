using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketMoves.Models.Enums
{
    public enum AlertStatus
    {

        [Display(Name = "On Deck")]
        OnDeck,
        Executed,
        Closed,
        Triggered
    }
}
