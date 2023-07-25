using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using StackExchange.Redis;

namespace FreeCourse.Services.Basket.Repository.Abstarct
{
    public interface IBasketRepository
    {
        public Task<bool> DeleteBasket(string userId);
        public Task<RedisValue> GetBasket(string userId);
        public Task<bool> SaveOrUpdate(BasketDto basket);
    }
}
