using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Basket.Services.Abstract
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasket(string userId);
        Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basket);
        Task<ResponseDto<bool>> DeleteBasket(string userId);
    }
}
