using Common.Entities;

namespace Catalog.Data.Repositories.EntityFramework.Abstract
{
    public interface IBrandRepository
    {
        Task<Brand?> GetBrandById(Guid id);
    }
}
