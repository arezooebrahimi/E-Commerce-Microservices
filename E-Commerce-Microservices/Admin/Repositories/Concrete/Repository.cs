using Admin.Dtos.Common;
using Admin.Repositories.Abstract;
using Common.Contexts;
using Common.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Admin.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CatalogDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(CatalogDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<(List<T> Items, int Total)> GetAllPaginateAsync(PagedRequest req)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (req.Filters != null && req.Filters.Any())
                query = DynamicFilterHelper.ApplyDynamicFilters(query, req.Filters);

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
