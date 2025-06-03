using Common.Dtos.Admin.ProductReview;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Abstract
{
    public interface IProductReviewService
    {
        Task<PagedResponse<GetProductReviewsPaginateDto>> GetAllPaginateAsync(PagedRequest request);
        Task<ProductReview> AddAsync(CreateProductReviewsRequest request);
        Task<ProductReview?> UpdateAsync(CreateProductReviewsRequest request);
        Task DeleteAsync(IEnumerable<ProductReview> brands);
        Task<ProductReview?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductReview>> GetAllByIdsAsync(List<Guid> ids);
    }
}
