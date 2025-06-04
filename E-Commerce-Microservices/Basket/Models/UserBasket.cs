namespace Basket.Models
{
    public record UserBasket
    {
        public UserBasket()
        {
            Items = new List<BasketItem>();
        }
        public List<BasketItem> Items { get; set; }
    }
}
