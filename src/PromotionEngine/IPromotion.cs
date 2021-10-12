using System.Collections.Generic;

namespace PromotionEngine
{
    public interface IPromotion
    {
        public string Label { get; }

        public bool IsApplicable(Dictionary<char, int> cart);

        public decimal CalculateSaving(Dictionary<char, int> cart);
    }
}
