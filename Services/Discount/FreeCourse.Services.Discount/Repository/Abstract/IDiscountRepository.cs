using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Repository.Abstract
{
    public interface IDiscountRepository
    {
        Task<List<Models.Discount>> GetAll();
        Task<Models.Discount> GetById(int id);
        Task<bool> Save(DiscountDto discountDto);
        Task<bool> Update(DiscountDto discountDto);
        Task<bool> DeleteById(int id);
        Task<Models.Discount> GetByCodeAndUserId(string code, string userId);
    }
}
