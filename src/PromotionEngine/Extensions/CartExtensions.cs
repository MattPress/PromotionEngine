using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Extensions
{
    internal static class CartExtensions
    {
        internal static decimal GetTotalCost(this Dictionary<Item, int> cart)
        {
            return cart.Sum(x => x.Key.Cost * x.Value);
        }
    }
}
