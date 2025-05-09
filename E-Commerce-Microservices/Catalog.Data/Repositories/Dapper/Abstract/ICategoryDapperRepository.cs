using Common.Entities;

namespace Catalog.Data.Repositories.Dapper.Abstract
{
    public interface ICategoryDapperRepository
    {
        Task<IEnumerable<Category>> GetHomePageCategoriesAsync();
    }
}
