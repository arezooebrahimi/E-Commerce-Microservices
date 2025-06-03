using Admin.Services.Abstract;
using Common.Dtos.Admin.Tag;
using Common.Dtos.Common;
using Common.Entities;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<PagedResponse<GetTagsPaginateDto>>> GetPaginate(PagedRequest request)
        {
            return Ok(await _service.GetAllPaginateAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<Tag>> GetById(Guid id)
        {
            var resp = await _service.GetByIdAsync(id);
            if (resp != null)
                return Ok(resp);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ApiResult<Tag>> Create(CreateTagRequest request)
        {
            var resp = await _service.AddAsync(request);
            return Ok(resp);
        }

        [HttpPut]
        public async Task<ApiResult<Tag>> Update([FromBody] CreateTagRequest request)
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
