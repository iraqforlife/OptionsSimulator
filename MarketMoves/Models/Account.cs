using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        public Account(string email) : base(email)
        {            
            Email = email;
            Suscribed = false;
            SuscriptionExpiration = DateTime.Now.AddDays(7);
        }
        public Account(string email, string tradingview)
            :this(email)
        {
            TradingViewUserName = tradingview;
        }
    }
}
