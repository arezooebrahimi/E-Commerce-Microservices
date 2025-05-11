using Admin.Dtos.Common;
using Common.Dtos.Catalog.Product;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Abstract
{
    public interface IProductService
    {
        Task<PagedResponse<GetProductsPaginateDto>> GetAllPaginateAsync(PagedRequest request);
        Task<Product> AddAsync(CreateProductRequest request);
        Task<Product?> UpdateAsync(CreateProductRequest request);
        Task DeleteAsync(IEnumerable<Product> products);
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllByIdsAsync(List<Guid> ids);
    }
}
