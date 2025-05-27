using Admin.Services.Abstract;
using Common.Dtos.Catalog.Product;
using Common.Dtos.Common;
using Common.Entities;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<PagedResponse<GetProductsPaginateDto>>> GetPaginate(PagedRequest request)
        {
            return Ok(await _service.GetAllPaginateAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<Product>> GetById(Guid id)
        {
            var resp = await _service.GetByIdAsync(id);
            if (resp != null)
                return Ok(resp);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ApiResult<Product>> Create(CreateProductRequest request)
        {
            var resp = await _service.AddAsync(request);
            return Ok(resp);
        }

        [HttpPut]
        public async Task<ApiResult<Product>> Update([FromBody] CreateProductRequest request)
        {
            var resp = await _service.UpdateAsync(request);
            if (resp != null)
                return Ok(resp);
            else
                return NotFound();
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(List<Guid> ids)
        {
            var entities = await _service.GetAllByIdsAsync(ids);
            if (!entities.Any())
                return NotFound();

            await _service.DeleteAsync(entities);
            return Ok();
        }
    }
}
