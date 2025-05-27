using Admin.Services.Abstract;
using Common.Dtos.Catalog.Brand;
using Common.Dtos.Common;
using Common.Entities;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<PagedResponse<GetBrandsPaginateDto>>> GetPaginate(PagedRequest request)
        {
            return Ok(await _service.GetAllPaginateAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<Brand>> GetById(Guid id)
        {
            var resp = await _service.GetByIdAsync(id);
            if (resp != null)
                return Ok(resp);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ApiResult<Brand>> Create(CreateBrandRequest request)
        {
            var resp = await _service.AddAsync(request);
            return Ok(resp);
        }

        [HttpPut]
        public async Task<ApiResult<Brand>> Update([FromBody] CreateBrandRequest request)
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
