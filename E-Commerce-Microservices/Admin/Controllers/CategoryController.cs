using Admin.Dtos.Common;
using Admin.Services.Abstract;
using Common.Dtos.Admin.Category;
using Common.Dtos.Common;
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
    }
}
