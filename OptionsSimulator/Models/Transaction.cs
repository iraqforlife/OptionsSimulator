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
        [Range(0.0, Double.MaxValue)]
        public double Entry { get; set; }
        [Range(0.0, Double.MaxValue)]
        public double Exit { get; set; }

        public double Gains { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfContracts { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue)]
        public double StrikePrice { get; set; }
        [Required]
        public DateTime Expiration { get; set; }

    }
}

