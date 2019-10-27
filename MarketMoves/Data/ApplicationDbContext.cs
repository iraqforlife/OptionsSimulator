using System;
using System.Collections.Generic;
using System.Text;
using MarketMoves.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketMoves.Data
{
    public class ApplicationDbContext : IdentityDbContext<Account>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
