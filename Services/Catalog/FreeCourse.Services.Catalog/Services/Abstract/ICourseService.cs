using FreeCourse.Services.Catalog.Dto;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Abstract
{
    public interface ICourseService
    {
        Task<ResponseDto<List<CourseDto>>> GetAllAsync();
        Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);
        Task<ResponseDto<List<CourseDto>>> GetByUserIdAsync(string userId);
        Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<ResponseDto<NoContent>> DeleteByIdAsync(string id);
    }
}
