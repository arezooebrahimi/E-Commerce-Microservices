using Common.Dtos.Admin.Product;
using Common.Entities;

namespace Catalog.Data.Repositories.EntityFramework.Abstract
{
    public interface IProductRepository
    {
        Task<(List<ProductsDto> Items, int Total)> GetProducts(GetProductsRequest req);
        Task<List<ProductsRaitingAndReviewsCountDto>> GetProductsRaitingAndReviewsCount(List<Guid> productIds);
        Task<Product?> GetProductDetailsBySlug(string slug);
        Task<List<FeatureOption>> GetFeatureOptionsByIds(List<Guid?> featureOptionIds);
    }
}
