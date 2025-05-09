using Admin.Dtos.Common;

namespace Admin.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<(List<T> Items, int Total)> GetAllPaginateAsync(PagedRequest req);
    }
}
