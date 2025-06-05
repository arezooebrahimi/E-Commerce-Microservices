using Catalog.Data.Repositories.EntityFramework.Abstract;
using Common.Contexts;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data.Repositories.EntityFramework.Concrete
{
    public class TagRepository: ITagRepository
    {
        private readonly CatalogDbContext _context;
        public TagRepository(CatalogDbContext context) 
        { 
            _context = context;
        }

        public async Task<Tag?> GetTagBySlug(string slug)
        {
            return await _context.Tags.Where(c => c.Slug == slug).FirstOrDefaultAsync();
        }
    }
}
