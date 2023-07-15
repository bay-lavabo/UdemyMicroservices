using FreeCourse.Services.Catalog.Model;

namespace FreeCourse.Services.Catalog.Repository.Abstract
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course> CreateAsync(Course course);
        Task<Course> GetByIdAsync(string id);
        Task<List<Course>> GetByUserIdAsync(string userId);
        Task<Course> UpdateAsync(Course course);
        Task<bool> DeleteByIdAsync(string id);
    }
}
