using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Repository.Abstarct;
using FreeCourse.Services.Basket.Repository.Context;
using StackExchange.Redis;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Repository.Concrete
{
    public class BasketRepository : IBasketRepository
    {
        private readonly RedisContext _redisContext;

        public BasketRepository(RedisContext redisContext)
        {
            _redisContext = redisContext;
        }

        public async Task<bool> DeleteBasket(string userId)
        {
            return await _redisContext.GetDb().KeyDeleteAsync(userId);
        }

        public async Task<RedisValue> GetBasket(string userId)
        {
            return await _redisContext.GetDb().StringGetAsync(userId);
        }

        public async Task<bool> SaveOrUpdate(BasketDto basket)
        {
            return await _redisContext.GetDb().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
        }
    }
}
