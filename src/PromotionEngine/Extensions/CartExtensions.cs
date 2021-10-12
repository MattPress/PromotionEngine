using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Extensions
{
    public static class CartExtensions
    {
        public static decimal GetTotalCost(this Dictionary<Item, int> cart)
        {
            return cart.Sum(x => x.Key.Cost * x.Value);
        }
    }
}
