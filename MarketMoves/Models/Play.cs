using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketMoves.Models
{
    public class Play
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public double Profit { get; set; }
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public double Entry { get; set; }
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public double Exit { get; set; }
        public string Why { get; set; }
        public string Execution { get; set; }
        public string Learning { get; set; }
        public string Title { get; set; }
        public string Proof { get; set; }
        public bool Locked { get; set; }

        public void Update(Play play)
        {
            AccountId = play.AccountId;
            Profit = play.Profit;
            Entry = play.Entry;
            Exit = play.Exit;
            Why = play.Why;
            Execution = play.Execution;
            Learning = play.Learning;
            Title = play.Title;
            Proof = play.Proof;
            Locked = play.Locked;
        }
    }
}
