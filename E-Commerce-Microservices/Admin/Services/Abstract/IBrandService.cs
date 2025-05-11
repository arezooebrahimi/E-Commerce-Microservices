using Admin.Dtos.Common;
using Common.Dtos.Catalog.Brand;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Abstract
{
    public interface IBrandService
    {
        Task<PagedResponse<GetBrandsPaginateDto>> GetAllPaginateAsync(PagedRequest request);
        Task<Brand> AddAsync(CreateBrandRequest request);
        Task<Brand?> UpdateAsync(CreateBrandRequest request);
        Task DeleteAsync(IEnumerable<Brand> brands);
        Task<Brand?> GetByIdAsync(Guid id);
        Task<IEnumerable<Brand>> GetAllByIdsAsync(List<Guid> ids);
    }
}
