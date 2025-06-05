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
            return await _context.Categories.Include(m=>m.Medias).Where(c=>c.DisplayOnHomePage).OrderByDescending(c=>c.Order).ToListAsync();
        }


        public async Task<Category?> GetCategoryBySlug(string slug)
        {
            return await _context.Categories.Include(c=>c.SubCategories).Where(c => c.Slug == slug).FirstOrDefaultAsync();
        }
    }
}
