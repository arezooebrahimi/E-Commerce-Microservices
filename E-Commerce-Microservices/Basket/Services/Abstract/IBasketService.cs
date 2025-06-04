using Basket.Models;

namespace Basket.Services.Abstract
{
    public interface IBasketService
    {
        Task<UserBasket?> GetBasketAsync(string basketId, bool createIfNotExist = false);
        Task<UserBasket?> AddItemAsync(RemoveBasketItemRequest item);
        Task<UserBasket?> RemoveItemAsync(RemoveBasketItemRequest req);
        Task<UserBasket> MergeBasketsAsync(string basketId, string userId);
    }
}
