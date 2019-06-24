using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketMoves.Models.Enums
{
    public enum AlertStatus
    {
        OnDeck,
        Executed,
        Closed
    }
    public static class AlertStatusExtensions
    {
        public static string GetDescription(this AlertStatus value)
        {
            switch (value)
            {
                case AlertStatus.OnDeck: return "On Deck";
                case AlertStatus.Executed: return "Executed";
                case AlertStatus.Closed: return "Closed";
                default: throw new NotImplementedException();
            }
        }
    }
}
