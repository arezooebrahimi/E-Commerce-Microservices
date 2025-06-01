using Common.Entities;

namespace Admin.Repositories.Abstract
{
    public interface ICategoryMediaRepository
    {
        void DeleteRange(IEnumerable<CategoryMedia> entity);
        Task<List<CategoryMedia>> GetByCategoryIdAsync(Guid? categoryId);
    }
}
