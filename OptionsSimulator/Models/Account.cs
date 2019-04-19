using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsSimulator.Models
{
    public class Account : IdentityUser
    {
        public double Balance { get; set; } // balance = equity+cash
        public double Equity { get; set; }
        public double Cash { get; set; }
        [Required]
        public double InitialBalance { get; set; }

        public List<Transaction> Transactions { get; }

    }
}
