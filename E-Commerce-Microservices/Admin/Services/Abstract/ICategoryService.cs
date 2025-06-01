using Common.Dtos.Admin.Category;
using Common.Dtos.Catalog.Category;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Abstract
{
    public interface ICategoryService
    {
        Task<PagedResponse<GetCategoriesPaginateDto>> GetAllPaginateAsync(PagedRequest request);
        Task<Category> AddAsync(CreateCategoryRequest category);
        Task<Category?> UpdateAsync(CreateCategoryRequest category);
        Task DeleteAsync(IEnumerable<Category> category);
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllByIdsAsync(List<Guid> ids);
        Task<List<SelectListResponse>> GetParentsForSelect();
    }
}
