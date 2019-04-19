using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsSimulator.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Open { get; set; }
        [DataType(DataType.Date)]
        public DateTime Close { get; set; }
        [Required]
        public double Entry { get; set; }
        public double Exit { get; set; }
        public double Gains { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public int NumberOfContracts { get; set; }
        [Required]
        public double StrikePrice { get; set; }
        [Required]
        public DateTime Expiration { get; set; }

    }
}

