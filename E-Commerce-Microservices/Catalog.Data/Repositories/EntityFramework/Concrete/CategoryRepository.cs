using Catalog.Data.Repositories.EntityFramework.Abstract;
using Common.Contexts;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data.Repositories.EntityFramework.Concrete
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly CatalogDbContext _context;
        public CategoryRepository(CatalogDbContext context) 
        { 
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetHomePageCategoriesAsync()
        {
            return await _context.Categories.Include(m=>m.Medias).Where(c=>c.DisplayOnHomePage).ToListAsync();
        }
    }
}
