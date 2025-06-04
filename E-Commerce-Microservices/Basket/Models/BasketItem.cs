namespace Basket.Models
{
    public record BasketItem
    {
        public string ProductId { get; set; } = default!;
        public int? Quantity { get; set; }
        public string? FeatureOptionId { get; set; }
    }
}
