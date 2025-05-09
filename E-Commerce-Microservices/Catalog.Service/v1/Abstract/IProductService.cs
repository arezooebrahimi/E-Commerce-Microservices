using Common.Dtos.Catalog.Product;

namespace Catalog.Service.v1.Abstract
{
    public interface IProductService
    {
        Task<ProductDetailsResponse?> GetProductDetails(string slug);
        Task<List<ProductsResponse>> GetProducts(GetProductsRequest req);
    }
}
