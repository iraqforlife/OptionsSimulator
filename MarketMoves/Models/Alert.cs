using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using MarketMoves.Models.Enums;
using System.ComponentModel;


namespace MarketMoves.Models
{
    public class Alert
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a title for the alert")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please enter the selected option")]
        public string Option { get; set; }
        [Required(ErrorMessage = "Please select option type")]
        public OptionType OptionType { get; set; }
        [Required(ErrorMessage = "Please enter the strike price")]
        public double Strike { get; set; } 
        [Required(ErrorMessage = "Please enter the desired entry")]
        public string Entry { get; set; }

        [Required(ErrorMessage = "Please enter the profit price ($)")]
        [Display(Name ="Profit Price")]
        public string ProfitPrice { get; set; }

        [Required(ErrorMessage = "Please enter the profit target (%)")]
        [Display(Name = "Profit Target")]
        public string ProfitTarget { get; set; }

        [Required(ErrorMessage = "Please enter the lost price ($)")]
        [Display(Name = "Loss Price")]
        public string LossPrice { get; set; }

        [Required(ErrorMessage = "Please enter the Loss target (%)")]
        [Display(Name = "Profit Target")]
        public string LossTarget { get; set; }

        [Required(ErrorMessage = "Please enter the risk:reward ratio")]
        [Display(Name = "Risk:Reward ratio")]
        public string RiskReward { get; set; }

        [Required(ErrorMessage = "Please enter the alert description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the first chart link")]
        [Display(Name = "First chart link")]
        public string Image1Link { get; set; }

        [Required(ErrorMessage = "Please enter the chart time frame")]
        [Display(Name = "First chart time frame")]
        public string Image1Description { get; set; }

        [Display(Name = "Second Chart Link")]
        public string Image2Link { get; set; }

        [Display(Name = "Second chart time frame")]
        public string Image2Description { get; set; }

        [Required]
        [Display(Name = "Current Alert Status")]
        public AlertStatus Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public DateTime Closed { get; set; }

        [Display(Name = "Executed entry price ($)")]
        public double ExecutedEntry { get; set; }
        [Display(Name = "Executed exit price ($)")]
        public double ExecutedExit { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }

        public Alert()
        {
            this.Status = AlertStatus.OnDeck;
            this.Created = DateTime.Now;
        }
    }
}
