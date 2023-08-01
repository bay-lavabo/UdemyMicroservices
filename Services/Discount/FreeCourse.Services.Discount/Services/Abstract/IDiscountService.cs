using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Services.Abstract
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<DiscountDto>>> GetAll();
        Task<ResponseDto<DiscountDto>> GetById(int id);
        Task<ResponseDto<NoContent>> Save(DiscountDto discountDto);
        Task<ResponseDto<NoContent>> Update(DiscountDto discountDto);
        Task<ResponseDto<NoContent>> DeleteById(int id);
        Task<ResponseDto<DiscountDto>> GetByCodeAndUserId(string code, string userId);
    }
}
