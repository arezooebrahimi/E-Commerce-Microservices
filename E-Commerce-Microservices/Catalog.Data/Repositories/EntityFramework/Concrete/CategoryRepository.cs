using Catalog.Data.Contexts;
using Catalog.Data.Repositories.EntityFramework.Abstract;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.Categories.Where(c=>c.DisplayOnHomePage).ToListAsync();
        }
    }
}
