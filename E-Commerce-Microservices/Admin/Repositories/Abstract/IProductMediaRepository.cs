using Common.Entities;

namespace Admin.Repositories.Abstract
{
    public interface IProductMediaRepository
    {
        void DeleteRange(IEnumerable<ProductMedia> entity);
        Task<List<ProductMedia>> GetByProductIdAsync(Guid? categoryId);
    }
}
