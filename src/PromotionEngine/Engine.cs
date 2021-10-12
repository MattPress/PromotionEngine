using PromotionEngine.Extensions;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class Engine
    {
        private readonly IEnumerable<IPromotion> _promotions;

        public Engine(IEnumerable<IPromotion> promotions)
        {
            _promotions = promotions ?? throw new ArgumentNullException(nameof(promotions));
        }

        public IEngineResult Run(Dictionary<Item, int> cart)
        {
            var result = new EngineResult(cart.GetTotalCost());
            foreach (var promotion in _promotions)
            {
                if (!promotion.IsApplicable(cart))
                    continue;

                result.AddPromotion(promotion.Label, promotion.CalculateSaving(cart));
                break;
            }
            return result;
        }
    }
}
