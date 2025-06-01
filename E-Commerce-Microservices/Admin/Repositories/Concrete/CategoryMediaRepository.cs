using Admin.Repositories.Abstract;
using Common.Contexts;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admin.Repositories.Concrete
{
    public class CategoryMediaRepository:ICategoryMediaRepository
    {
        private readonly CatalogDbContext _context;

        public CategoryMediaRepository(CatalogDbContext context)
        {
            _context = context;
        }


        public void DeleteRange(IEnumerable<CategoryMedia> entity)
        {
            _context.CategoryMedias.RemoveRange(entity);
            _context.SaveChanges();
        }

        public async Task<List<CategoryMedia>> GetByCategoryIdAsync(Guid? categoryId)
        {
            var entities = await _context.CategoryMedias.Where(c=>c.CategoryId == categoryId).ToListAsync();
            return entities;
        }
    }
}
