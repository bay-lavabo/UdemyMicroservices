using Dapper;
using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Services.Discount.Repository.Abstract;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Repository.Concrete
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<bool> DeleteById(int id)
        {
            var status = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id = @Id", new { Id = id });

            return status > 0 ? true : false;
        }

        public async Task<List<Models.Discount>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount");

            return discounts.ToList();
        }

        public async Task<Models.Discount> GetByCodeAndUserId(string code, string userId)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Models.Discount>("SELECT * FROM discount WHERE code = @Code AND userid = @UserId", new { Code = code, UserId = userId });
        }

        public async Task<Models.Discount> GetById(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Models.Discount>("select * from discount where id = @Id", new { Id = id });
        }

        public async Task<bool> Save(DiscountDto discountDto)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES (@UserId, @Rate, @Code) ", discountDto);

            return saveStatus > 0 ? true : false;
        }

        public async Task<bool> Update(DiscountDto discountDto)
        {
            var updateStatus = await _dbConnection.ExecuteAsync("UPDATE discount SET userid = @UserId, code = @Code, rate = @Rate WHERE id = @Id", new
            {
                Id = discountDto.Id,
                UserId = discountDto.UserId,
                Code = discountDto.Code,
                Rate = discountDto.Rate
            });

            return updateStatus > 0 ? true : false;
        }
    }
}
