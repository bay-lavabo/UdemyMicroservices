using AutoMapper;
using FreeCourse.Services.Catalog.Dto;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Repository.Abstract;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Repository.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        public async Task<List<Category>> GetAllAsync()
        {
           return await _categoryCollection.Find(category => true).ToListAsync();
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);

            return category;
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            return category;
        }

    }
}
