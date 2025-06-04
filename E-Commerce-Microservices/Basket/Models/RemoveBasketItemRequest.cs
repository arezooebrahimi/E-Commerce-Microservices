

namespace Basket.Models
{
    public class RemoveBasketItemRequest
    {
        public string? BasketId { get; set; }
        public string ProductId { get; set; } = default!;
        public int? Quantity { get; set; }
        public string? FeatureOptionId { get; set; }
    }
}
