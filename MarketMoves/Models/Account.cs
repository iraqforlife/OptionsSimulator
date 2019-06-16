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
        public double Balance { get; set; } // balance = equity+cash
        public double Equity { get; set; }
        public double Cash { get; set; }
        [Required]
        public double InitialBalance { get; set; }

        public List<Transaction> Transactions { get; private set; }

        public void AddTransaction(Transaction transaction)
        {
            if (Transactions == null)
                Transactions = new List<Transaction>();

            Transactions.Add(transaction);

            if(transaction.Gains != 0)
            {
                Balance += transaction.Gains;
            }
            if(transaction.Exit == 0)
            {
                Equity += transaction.Entry * transaction.NumberOfContracts;
                Cash -= transaction.Entry * transaction.NumberOfContracts;
            }

        }
    }
}
