using Asp.Versioning;
using Catalog.Service.v1.Abstract;
using Common.Dtos.Catalog.Category;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    [ApiVersion(1)]
    [Route("catalog/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]")]
        public async Task<ApiResult<IEnumerable<CategoryHomePageResponse>>> GetHomePageCategories()
        {
            var categories = await _categoryService.GetHomePageCategoriesAsync();
            return Ok(categories);
        }



        [HttpGet("[action]/{slug}")]
        public async Task<ApiResult<GetCategoryBySlugResponse>> GetBySlug(string slug)
        {
            var category = await _categoryService.GetCategoryBySlug(slug);
            return Ok(category);
        }
    }
}
