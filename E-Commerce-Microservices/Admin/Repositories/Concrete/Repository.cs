using Admin.Dtos.Common;
using Admin.Repositories.Abstract;
using Common.Contexts;
using Common.Entities.Abstract;
using Common.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Admin.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly CatalogDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(CatalogDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }


        public async Task<IEnumerable<T>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await _dbSet.Where(t => ids.Contains(t.Id)).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsSlugDuplicateAsync(string slug)
        {
            if (!typeof(ISlugEntity).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException($"Type {typeof(T).Name} does not have a Slug property.");

            return await _dbSet
                .Cast<ISlugEntity>()
                .AnyAsync(e => e.Slug == slug);
        }


        public async Task<(List<T> Items, int Total)> GetAllPaginateAsync(PagedRequest req)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if(req.Includes!=null)
               query = DynamicIncludeHelper.ApplyIncludes(query,req.Includes);

            if (req.Filters != null && req.Filters.Any())
                query = DynamicFilterHelper.ApplyDynamicFilters(query, req.Filters!);

            var total = await query.CountAsync();

            if (!string.IsNullOrEmpty(req.Sort?.Column))
                query = DynamicSortHelper.ApplySorting(query, req.Sort);

            query = query
                .Skip((req.Page - 1) * req.PerPage)
                .Take(req.PerPage);

            var items = await query.ToListAsync();
            return (items, total);
        }
    }
}
