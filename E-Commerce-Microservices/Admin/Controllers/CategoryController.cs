using Admin.Services.Abstract;
using Common.Dtos.Admin.Category;
using Common.Dtos.Catalog.Category;
using Common.Dtos.Common;
using Common.Entities;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;


namespace Admin.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<PagedResponse<GetCategoriesPaginateDto>>> GetPaginate(PagedRequest request)
        {
            return Ok(await _service.GetAllPaginateAsync(request));
        }



        [HttpGet("{id}")]
        public async Task<ApiResult<Category>> GetById(Guid id)
        {
            var resp = await _service.GetByIdAsync(id);
            if (resp != null)
                return Ok(resp);
            else
                return NotFound();
        }


        [HttpPost]
        public async Task<ApiResult<Category>> Create([FromBody] CreateCategoryRequest request)
        {
            var resp = await _service.AddAsync(request);
            return Ok(resp);
        }


        [HttpPut]
        public async Task<ApiResult<Category>> Update([FromBody] CreateCategoryRequest request)
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
            if (entities.Count() == 0)
                return NotFound();

            await _service.DeleteAsync(entities);
            return Ok();
        }
    }
}
