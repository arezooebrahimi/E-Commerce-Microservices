using Admin.Services.Abstract;
using Common.Dtos.Admin.FeatureOption;
using Common.Dtos.Common;
using Common.Entities;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    public class FeatureOptionController : ControllerBase
    {
        private readonly IFeatureOptionService _service;

        public FeatureOptionController(IFeatureOptionService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<PagedResponse<GetFeatureOptionsPaginateDto>>> GetPaginate(PagedRequest request)
        {
            return Ok(await _service.GetAllPaginateAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<FeatureOption>> GetById(Guid id)
        {
            var resp = await _service.GetByIdAsync(id);
            if (resp != null)
                return Ok(resp);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ApiResult<FeatureOption>> Create(CreateFeatureOptionRequest request)
        {
            var resp = await _service.AddAsync(request);
            return Ok(resp);
        }

        [HttpPut]
        public async Task<ApiResult<FeatureOption>> Update([FromBody] CreateFeatureOptionRequest request)
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
