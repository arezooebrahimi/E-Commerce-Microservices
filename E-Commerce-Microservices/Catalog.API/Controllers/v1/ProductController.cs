using Asp.Versioning;
using Catalog.API.WebFramework.Api;
using Catalog.Service.v1.Abstract;
using Common.Dtos.Catalog.Product;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productServic)
        {
            _productService = productServic;
        }


        [HttpPost("[action]")]
        public async Task<ApiResult<List<ProductsResponse>>> Get(GetProductsRequest req)
        {
            var response = await _productService.GetProducts(req);
            return Ok(response);
        }


        [HttpGet("[action]/{slug}")]
        public async Task<ApiResult<ProductDetailsResponse>> Get(string slug)
        {
            var response = await _productService.GetProductDetails(slug);
            return Ok(response);
        }
    }
}
