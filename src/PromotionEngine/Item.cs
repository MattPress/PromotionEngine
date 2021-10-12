namespace PromotionEngine
{
    public class Item
    {
        public char SkuId { get; }

        public decimal Cost { get; }

        public Item(char skuId, decimal cost)
        {
            SkuId = skuId;
            Cost = cost;
        }
    }
}
