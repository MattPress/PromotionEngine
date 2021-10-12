using System;

namespace PromotionEngine
{
    internal class EngineResult : IEngineResult
    {
        private readonly decimal _defaultCartTotal;

        public bool HasApplicablePromotion => !string.IsNullOrWhiteSpace(PromotionApplied);

        public string PromotionApplied { get; private set; }

        public decimal PromotionSaving { get; private set; }

        public decimal Total => _defaultCartTotal - PromotionSaving;

        public EngineResult(decimal defaultCartTotal)
        {
            _defaultCartTotal = defaultCartTotal;
        }

        public void AddPromotion(string promotionApplied, decimal promotionSaving)
        {
            if (string.IsNullOrWhiteSpace(promotionApplied)) throw new ArgumentNullException(nameof(promotionApplied));
            if (promotionSaving <= 0 || promotionSaving > _defaultCartTotal) throw new ArgumentOutOfRangeException(nameof(promotionSaving));

            if (!string.IsNullOrEmpty(PromotionApplied) || PromotionSaving > 0) 
                throw new InvalidOperationException($"Promotion '{nameof(promotionApplied)}' cannot be applied. Promotion '{PromotionApplied}' has already been applied and promotions are mutually exclusive.");

            PromotionApplied = promotionApplied;
            PromotionSaving = promotionSaving;
        }
    }
}
