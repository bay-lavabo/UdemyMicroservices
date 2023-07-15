using AutoMapper;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Repository.Abstract;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Repository.Concrete
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IMongoCollection<Course> _courseCollection;

        public CourseRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _courseCollection.Find(category => true).ToListAsync();
        }

        public async Task<Course> CreateAsync(Course course)
        {
            await _courseCollection.InsertOneAsync(course);

            return course;
        }

        public async Task<Course> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();

            return course;
        }

        public async Task<List<Course>> GetByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

            return courses;
        }

        public async Task<Course> UpdateAsync(Course course)
        {
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == course.Id, course);

            return result;

        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x  => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
