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
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone numer must be numbers only and match: 5415486675")]
        [StringLength(10, MinimumLength = 10)]
        [MaxLength(10)]
        [MinLength(10)]
        [Display(Name = "Phone number")]
        public override string PhoneNumber { get; set; }

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
