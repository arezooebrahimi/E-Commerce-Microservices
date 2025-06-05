using Catalog.Data.Repositories.EntityFramework.Abstract;
using Common.Contexts;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data.Repositories.EntityFramework.Concrete
{
    public class BrandRepository: IBrandRepository
    {
        private readonly CatalogDbContext _context;
        public BrandRepository(CatalogDbContext context) 
        { 
            _context = context;
        }

        public async Task<Brand?> GetBrandById(Guid id)
        {
            return await _context.Brands.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
