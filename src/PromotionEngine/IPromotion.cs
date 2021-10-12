using System.Collections.Generic;

namespace PromotionEngine
{
    public interface IPromotion
    {
        public string Label { get; }

        public bool IsApplicable(Dictionary<Item, int> cart);

        public decimal CalculateSaving(Dictionary<Item, int> cart);
    }
}
