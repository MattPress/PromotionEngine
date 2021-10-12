using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Promotions
{
    public abstract class Promotion : IPromotion
    {
        public abstract string Label { get; }

        public abstract decimal CalculateSaving(Dictionary<Item, int> cart);

        public abstract bool IsApplicable(Dictionary<Item, int> cart);

        protected static bool TryGetItemCountFromCart(char skuId, Dictionary<Item, int> cart, out Item item, out int count)
        {
            var key = cart.Keys.FirstOrDefault(x => x.SkuId == skuId);
            item = key;
            count = key == null ? 0 : cart[key];
            return key == null;
        }
    }
}
