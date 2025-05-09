using Common.Dtos.Catalog.Category;


namespace Catalog.Service.v2.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryHomePageResponse>> GetHomePageCategoriesAsync();
    }
}
