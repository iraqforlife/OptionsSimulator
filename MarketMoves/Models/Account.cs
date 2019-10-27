using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketMoves.Models
{
    public class Account : IdentityUser
    {
        public string TradingViewUserName { get; set; }
        public string DiscordUserName { get; set; }
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

        public string Name { get; set; }
        public bool GetSmsNotification{ get; set; }

        public double Balance { get; set; }
        public List<Play> Plays { get; set; }
        public Account(string email, string name) : base(email)
        {            
            Email = email;
            Name = name;
            Suscribed = false;
            Balance = 0;
            SuscriptionExpiration = DateTime.Now.AddDays(7);
        }
        public Account(string email, string name, string tradingview)
            :this(email, name)
        {
            TradingViewUserName = tradingview;
        }
    }
}
