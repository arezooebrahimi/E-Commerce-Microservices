using Catalog.Data.Repositories.Dapper.Abstract;
using Catalog.Data.Repositories.Dapper.Queries;
using Common.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Catalog.Data.Repositories.Dapper.Concrete
{
    public class CategoryDapperRepository:ICategoryDapperRepository
    {
        private readonly string _connectionString;
        public CategoryDapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Category>> GetHomePageCategoriesAsync()
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            var categories = await connection.QueryAsync<Category>(
                CategoryQueries.GetHomePageCategories);

            return categories;
        }
    }
}
