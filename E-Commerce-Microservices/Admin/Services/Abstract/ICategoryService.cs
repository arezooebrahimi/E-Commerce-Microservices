using Admin.Dtos.Common;
using Common.Dtos.Admin.Category;
using Common.Dtos.Common;

namespace Admin.Services.Abstract
{
    public interface ICategoryService
    {
        Task<PagedResponse<GetCategoriesPaginateDto>> GetAllPaginateAsync(PagedRequest request);
    }
}
