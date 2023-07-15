using FreeCourse.Services.Catalog.Dto;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
    }
}
