using Common.Dtos.Catalog.Category;


namespace Catalog.Service.v1.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryHomePageResponse>> GetHomePageCategoriesAsync();
    }
}
