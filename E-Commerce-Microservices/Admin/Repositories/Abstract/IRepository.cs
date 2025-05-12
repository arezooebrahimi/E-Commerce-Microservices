using Admin.Dtos.Common;

namespace Admin.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllByIdsAsync(List<Guid> ids);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
        Task SaveChangesAsync();
        Task<(List<T> Items, int Total)> GetAllPaginateAsync(PagedRequest req);
        Task<bool> IsSlugDuplicateAsync(string slug);
    }
}
