using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketMoves.Models
{
    public class Account : IdentityUser
    {
        public string TradingViewUserName { get; set; }
        public bool Suscribed { get; set; }
        public DateTime SuscriptionExpiration { get; set; }

    }
}
