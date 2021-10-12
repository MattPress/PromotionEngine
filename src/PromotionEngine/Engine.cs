using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class Engine
    {
        private readonly IEnumerable<IPromotion> _promotions;

        public Engine(IEnumerable<IPromotion> promotions)
        {
            _promotions = promotions;
        }

        public IEngineResult Run(Dictionary<Item, int> cart)
        {
            throw new NotImplementedException();
        }
    }
}
