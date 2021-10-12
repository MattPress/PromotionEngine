using System.Collections.Generic;

namespace PromotionEngine.Promotions
{
    internal class OneCOneDCombinationPromotion : Promotion
    {
        public override string Label => nameof(OneCOneDCombinationPromotion);

        public override decimal CalculateSaving(Dictionary<Item, int> cart)
        {
            if (!TryGetItemCountFromCart('C', cart, out var _, out var cCount)
                || !TryGetItemCountFromCart('D', cart, out var _, out var dCount))
                return 0;

            return cCount > dCount
                ? dCount * 5
                : cCount * 5;
        }

        public override bool IsApplicable(Dictionary<Item, int> cart)
        {
            return TryGetItemCountFromCart('C', cart, out _, out var cCount) 
                && TryGetItemCountFromCart('D', cart, out _, out var dCount) 
                && cCount >= 1
                && dCount >= 1;
        }
    }
}
