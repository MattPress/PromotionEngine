namespace PromotionEngine
{
    public interface IEngineResult
    {
        public bool HasApplicablePromotion { get; }
        public string PromotionApplied { get; }
        public decimal PromotionSaving { get; }
        public decimal Total { get; }
    }
}
