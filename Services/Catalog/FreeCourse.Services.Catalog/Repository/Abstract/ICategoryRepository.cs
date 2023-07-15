using FreeCourse.Services.Catalog.Model;

namespace FreeCourse.Services.Catalog.Repository.Abstract
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category category);
        Task<Category> GetByIdAsync(string id);
    }
}
