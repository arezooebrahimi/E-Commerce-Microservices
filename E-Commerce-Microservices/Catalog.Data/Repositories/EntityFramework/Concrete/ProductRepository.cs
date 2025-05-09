using Catalog.Data.Contexts;
using Catalog.Data.Repositories.EntityFramework.Abstract;
using Common.Dtos.Catalog.Product;
using Common.Entities;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Data.Repositories.EntityFramework.Concrete
{
    public class ProductRepository: IProductRepository
    {
        private readonly CatalogDbContext _context;
        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }


        public async Task<List<ProductsDto>> GetProducts(GetProductsRequest req)
        {
            var products = await _context.Products
                .Include(v=>v.Variables.OrderBy(v=>v.Price).Take(1))
                .Where(v=>!v.IsDeleted)
                .Select(p=> new ProductsDto
                {
                   Id =  p.Id,
                   Slug = p.Slug,
                   Type = p.Type,
                   Name = p.Name,
                   Price = p.Price,
                   SalePrice = p.SalePrice,
                   DateOnSaleFrom = p.DateOnSaleFrom,
                   DateOnSaleTo = p.DateOnSaleTo,
                   Variables = p.Variables
                }).Take(req.Limit).Skip(req.Offset).AsNoTracking().ToListAsync();

            return products;
        }

        public async Task<Product?> GetProductDetailsBySlug(string slug)
        {
            var product = await _context.Products
                .Include(p=>p.Brand)
                .Include(p => p.Features).ThenInclude(f=>f.Feature)
                .Include(p => p.Medias)
                .Include(p => p.Reviews.OrderByDescending(r=>r.CreatedAt).Take(10))
                .Include(p => p.RelatedProducts)
                .Include(p=>p.Variables).ThenInclude(p=>p.FeatureOption)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDeleted);

            return product;
        }


        public async Task<List<FeatureOption>> GetFeatureOptionsByIds(List<Guid?> featureOptionIds)
        {
            var featureOptions = await _context.FeatureOptions
                    .Where(f => featureOptionIds.Contains(f.Id))
                    .AsNoTracking().ToListAsync();

            return featureOptions;
        }


        public async Task<List<ProductsRaitingAndReviewsCountDto>> GetProductsRaitingAndReviewsCount(List<Guid> productIds)
        {
            var commentsAndRating = await _context.ProductReviews
                .Where(r => productIds.Contains(r.ProductId))
                .GroupBy(r => r.ProductId)
                .Select(g => new ProductsRaitingAndReviewsCountDto
                {
                    ProductId = g.Key,
                    ReviewsCount = g.Count(),
                    Raiting = g.Average(r => r.Rating)
                }).AsNoTracking().ToListAsync();


            return commentsAndRating;
        }
    }
}
