using Common.Dtos.Catalog.Product;
using Common.Dtos.Common;

namespace Catalog.Service.v1.Abstract
{
    public interface IProductService
    {
        Task<ProductDetailsResponse?> GetProductDetails(string slug);
        Task<PagedResponse<ProductsResponse>> GetProducts(GetProductsRequest req);
    }
}
