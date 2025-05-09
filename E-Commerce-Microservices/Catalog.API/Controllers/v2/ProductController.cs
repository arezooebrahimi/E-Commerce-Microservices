using Asp.Versioning;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v2
{
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("[action]")]
        public ApiResult<string> Get()
        {
            //var y = 0;
            //var x = 5 / y;
            return Ok("get all products");
        }

        [HttpGet("[action]")]
        public ApiResult<string> GetById()
        {
            return Ok("get product by id");
        }

        [HttpPost("[action]")]
        public ApiResult<string> Create()
        {
            return Ok("create a new product");
        }


        [HttpPut("[action]")]
        public ApiResult<string> Update()
        {
            return Ok("update product by id");
        }


        [HttpDelete("[action]")]
        public ApiResult<string> Delete()
        {
            return Ok("delete product by id");
        }

    }
}
