using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Repository.Context;
using FreeCourse.Services.Basket.Services.Abstract;
using FreeCourse.Shared.Dtos;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly RedisContext _redisContext;

        public BasketService(RedisContext redisContext)
        {
            _redisContext = redisContext;
        }

        public async Task<ResponseDto<bool>> DeleteBasket(string userId)
        {
            var status = await _redisContext.GetDb().KeyDeleteAsync(userId);

            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket could not found.", 404);
        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
        {
            var exitstBasket = await _redisContext.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(exitstBasket))
            {
                return ResponseDto<BasketDto>.Fail("Basket not found", 404);
            }

            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(exitstBasket), 200);
        }

        public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basket)
        {
            var status = await _redisContext.GetDb().StringSetAsync(basket.UserId,JsonSerializer.Serialize(basket));

            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket could not update or save.", 500);
        }
    }
}
