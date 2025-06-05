using Catalog.Data.Repositories.EntityFramework.Abstract;
using Catalog.Service.v1.Abstract;
using Catalog.Service.v1.Grpc;
using Common.Dtos.Admin.Product;
using Common.Dtos.Common;
using Common.Entities;
using Common.Utilities;
using GetFiles.Grpc;



namespace Catalog.Service.v1.Concrete
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly GetFilesGrpcClient _filesClient;
        public ProductService(IProductRepository productRepository, GetFilesGrpcClient filesClient)
        {
            _productRepository = productRepository;
            _filesClient = filesClient;
        }


        public async Task<PagedResponse<ProductsResponse>> GetProducts(GetProductsRequest req)
        {
            var response = new PagedResponse<ProductsResponse>();
            long? price = 0;
            long? salePrice = 0;
            var now = DateTime.Now;
            var mediaIds = new List<string>();
            var medias = new List<MediaDocument>();
            ProductMedia? productMedia = null;
            string? mediaId = null;

            var (products, total) = await _productRepository.GetProducts(req);

            foreach (var item in products)
            {
                mediaId = item.Medias?.Where(m => m.IsPrimary).Select(m => m.MediaId).FirstOrDefault();
                if (mediaId != null)
                    mediaIds.Add(mediaId);
            }

            if (mediaIds.Count > 0)
                medias = await _filesClient.GetFilesByIds(mediaIds);

            var commentsAndRating = await _productRepository.GetProductsRaitingAndReviewsCount(products.Select(p => p.Id).ToList());

            foreach (var product in products)
            {
                price = product.Price;
                salePrice = product.DateOnSaleFrom <= now && now <= product.DateOnSaleTo ? product.SalePrice : product.Price;

                if (product.Type != 1 && product.Variables.Count() != 0)
                {
                    price = product.Variables.First().Price;
                    salePrice = product.Variables.First().DateOnSaleFrom <= now && now <= product.Variables.First().DateOnSaleTo ? product.Variables.First().SalePrice : product.Variables.First().Price;
                }

                productMedia = product.Medias?.Where(m => m.IsPrimary).FirstOrDefault();

                response.Items.Add(new ProductsResponse
                {
                    Name = product.Name,
                    Slug = product.Slug,
                    Price = price,
                    SalePrice = salePrice,
                    Raiting = commentsAndRating.Where(p => p.ProductId == product.Id).First().Raiting,
                    ReviewsCount = commentsAndRating.Where(p => p.ProductId == product.Id).First().ReviewsCount,
                    FilePath = productMedia != null ? medias.Where(m => m.Id == productMedia.MediaId).Select(m => m.Formats.Where(f => f.Format == "small").Select(f => f.FilePath).FirstOrDefault()).FirstOrDefault() : null,
                    AltText = productMedia?.AltText ?? null,
                });
            }
            response.Total = total;
            return response;
        }

        public async Task<ProductDetailsResponse?> GetProductDetails(string slug)
        {
            List<FeatureOption> featureOptions = new List<FeatureOption>();
            var product = await _productRepository.GetProductDetailsBySlug(slug);
            if (product == null)
                return null;

            if (product.Features != null)
            {
                featureOptions = await _productRepository.GetFeatureOptionsByIds(product.Features.Select(pf => pf.DefaultFeatureOptionId).ToList());
            }

            var commentsAndRating = await _productRepository.GetProductsRaitingAndReviewsCount([product.Id]);

            var now = DateTime.Now;
            var response = new ProductDetailsResponse
            {
                Name = product.Name,
                Slug = product.Slug,
                brand = product.Brand?.Name ?? "",
                Description = product.Description,
                ReviewsCount = commentsAndRating.Count != 0 ? commentsAndRating.First().ReviewsCount : 0,
                AverageRating = commentsAndRating.Count != 0 ? commentsAndRating.First().Raiting : 0,
                Price = product.Price,
                SalePrice = product.DateOnSaleFrom <= now && now <= product.DateOnSaleTo ? product.SalePrice : product.Price,
                DiscountPercent = product.SalePrice.HasValue ? Math.Round((double)(product.Price - product.SalePrice.Value) * 100 / product.Price, 2) : 0,
                Tag = product.Tag,
                StockStatus = product.StockStatus,
                LatestReviews = product.Reviews != null ? product.Reviews.Select(f => new ProductReviewDto
                {
                    Title = f.Title,
                    ReviewText = f.ReviewText,
                    Rating = f.Rating,
                    CreatedAt = DateTimeExtensions.ToPersianDate(f.CreatedAt),
                }).ToList() : null,

                Features = product.Features != null
                    ? product.Features
                        .Where(f => !f.IsVisible && !f.IsVariable)
                        .Select(f => new ProductFeatureDto
                        {
                            FeatureName = f.Feature?.Name ?? "",
                            OptionId = f.DefaultFeatureOptionId ?? Guid.Empty,
                            OptionName = f.DefaultFeatureOptionId != null ? featureOptions.Where(o => o.Id == f.DefaultFeatureOptionId).Select(o => o.Name).First() : "",
                        })
                        .ToList()
                    : null,

                Variables = product.Variables != null ? product.Variables
                    .Select(f => new ProductVariableDto
                    {
                        OptionName = f.FeatureOption?.Name ?? "",
                        Price = f.Price,
                        SalePrice = f.DateOnSaleFrom <= now && now <= f.DateOnSaleTo ? f.SalePrice : f.Price,
                        StockQuantity = f.StockQuantity,
                        StockStatus = f.StockStatus,
                        Weight = f.Width,
                        Length = f.Length,
                        Height = f.Height,
                        Width = f.Width,
                        Description = f.Description
                    }).ToList() : null,

                Seo = new SeoDto
                {
                    SeoTitle = product.SeoTitle,
                    MetaDescription = product.MetaDescription,
                    IsIndexed = product.IsIndexed,
                    IsFollowed = product.IsFollowed,
                    CanonicalUrl = product.CanonicalUrl
                }
            };
            return response;
        }
    }
}
