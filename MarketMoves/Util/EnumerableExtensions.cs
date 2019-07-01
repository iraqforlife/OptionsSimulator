using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketMoves.Util
{
    public static class EnumerableExtensions
    {
        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultVal)
        {
            if (source == null || !source.Any())
                return defaultVal;

            return source.Max(selector);
        }
    }
}
