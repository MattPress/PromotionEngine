using System;
using System.Collections.Generic;

namespace PromotionEngine.Promotions
{
    public class TwoBMultibuyPromotion : Promotion
    {
        public override string Label => nameof(TwoBMultibuyPromotion);

        public override decimal CalculateSaving(Dictionary<Item, int> cart)
        {
            if (!TryGetItemCountFromCart('B', cart, out var item, out var count))
                return 0;

            return Math.DivRem(count, 2, out _) * 15;
        }

        public override bool IsApplicable(Dictionary<Item, int> cart)
        {
            return TryGetItemCountFromCart('B', cart, out _, out var count) && count >= 2;
        }
    }
}
