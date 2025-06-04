using Basket.Models;
using Basket.Services.Abstract;
using StackExchange.Redis;
using System.Text.Json;

namespace Basket.Services.Concrete
{
    public class BasketService: IBasketService
    {
        private readonly IDatabase _db;

        public BasketService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        private static string GetBasketKey(string basketId) => $"basket:{basketId}";

        public async Task<UserBasket?> GetBasketAsync(string basketId,bool createIfNotExist = false)
        {
            var data = await _db.StringGetAsync(GetBasketKey(basketId));
            if (data.IsNullOrEmpty)
            {
                if (createIfNotExist)
                {
                    await _db.StringSetAsync(GetBasketKey(basketId), JsonSerializer.Serialize(new UserBasket()));
                    return new UserBasket();
                }
                else return null;
            }
            else
                return JsonSerializer.Deserialize<UserBasket>(data!);
        }


        public async Task<UserBasket?> RemoveItemAsync(RemoveBasketItemRequest item)
        {
            var basket = await GetBasketAsync(item.BasketId!);
            if (basket is not null)
            {
                if (item.Quantity != null)
                {
                    var existingItem = basket.Items.FirstOrDefault(x => x.ProductId == item.ProductId && x.FeatureOptionId == item.FeatureOptionId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity -= item.Quantity;
                        if(existingItem.Quantity == 0)
                            basket.Items.Remove(existingItem);
                    }
                }
                else
                {
                    basket.Items.RemoveAll(i => i.ProductId == item.ProductId && i.FeatureOptionId == item.FeatureOptionId);
                }

                var json = JsonSerializer.Serialize(basket);
                await _db.StringSetAsync(GetBasketKey(item.BasketId!), json);
            }
            return basket;
        }

        public async Task<UserBasket?> AddItemAsync(RemoveBasketItemRequest req)
        {
            var basket = await GetBasketAsync(req.BasketId!) ?? new UserBasket();
            if (basket is not null)
            {
                var existingItem = basket.Items.FirstOrDefault(x => x.ProductId == req.ProductId && x.FeatureOptionId == req.FeatureOptionId);

                if (existingItem != null)
                {
                    existingItem.Quantity += req.Quantity;
                }
                else
                {
                    basket.Items.Add(new BasketItem
                    {
                        ProductId = req.ProductId,
                        Quantity = req.Quantity,
                        FeatureOptionId = req.FeatureOptionId
                    });
                }

                await _db.StringSetAsync(GetBasketKey(req.BasketId!), JsonSerializer.Serialize(basket));
            }
              
            return basket;
        }

        public async Task<UserBasket> MergeBasketsAsync(string basketId,string userId)
        {
            var guestBasket = await GetBasketAsync(basketId);
            var userBasket = await GetBasketAsync(userId) ?? new UserBasket();
            if (guestBasket != null)
            {
                foreach (var guestItem in guestBasket.Items)
                {
                    var existingItem = userBasket.Items.FirstOrDefault(x =>
                        x.ProductId == guestItem.ProductId && x.FeatureOptionId == guestItem.FeatureOptionId);

                    if (existingItem != null)
                    {
                        existingItem.Quantity += guestItem.Quantity;
                    }
                    else
                    {
                        userBasket.Items.Add(guestItem);
                    }
                }

                await _db.StringSetAsync(GetBasketKey(userId), JsonSerializer.Serialize(userBasket));
                await _db.KeyDeleteAsync(GetBasketKey(basketId));
            }

            return userBasket;
        }
    }
}
