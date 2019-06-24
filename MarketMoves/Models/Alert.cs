using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MarketMoves.Models
{
    public class Alert
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Entry { get; set; }
        [Required]
        public string ProfitPrice { get; set; }
        [Required]
        public string ProfitTarget { get; set; }
        [Required]
        public string LossPrice { get; set; }
        [Required]
        public string LossTarget { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Option { get; set; }
        [Required]
        public string RiskReward { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image1Link { get; set; }
        public string Image1Description { get; set; }
        public string Image2Link { get; set; }
        public string Image2Description { get; set; }
        [Required]
        public Enums.AlertStatus Status { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime Closed { get; set; }

        double ExecutedEntry { get; set; }
        double ExecutedExit { get; set; }

    }
}
