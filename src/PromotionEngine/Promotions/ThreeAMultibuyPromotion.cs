using System;
using System.Collections.Generic;

namespace PromotionEngine.Promotions
{
    public class ThreeAMultibuyPromotion : Promotion
    {
        public override string Label => nameof(ThreeAMultibuyPromotion);

        public override decimal CalculateSaving(Dictionary<Item, int> cart)
        {
            if (!TryGetItemCountFromCart('A', cart, out var item, out var count))
                return 0;

            return Math.DivRem(count, 3, out _) * 20;
        }

        public override bool IsApplicable(Dictionary<Item, int> cart)
        {
            return TryGetItemCountFromCart('A', cart, out _, out var count) && count >= 3;
        }
    }
}
