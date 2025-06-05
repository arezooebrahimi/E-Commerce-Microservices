using Admin.Repositories.Abstract;
using Common.Contexts;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admin.Repositories.Concrete
{
    public class ProductMediaRepository : IProductMediaRepository
    {
        private readonly CatalogDbContext _context;

        public ProductMediaRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public void DeleteRange(IEnumerable<ProductMedia> entity)
        {
            _context.ProductMedias.RemoveRange(entity);
            _context.SaveChanges();
        }

        public async Task<List<ProductMedia>> GetByProductIdAsync(Guid? productId)
        {
            var entities = await _context.ProductMedias.Where(c=>c.ProductId == productId).ToListAsync();
            return entities;
        }
    }
}
