using Common.Entities.Enums;


namespace Common.Dtos.Catalog.Product
{
    public class ProductDetailsResponse
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? brand { get; set; }
        public int ReviewsCount { get; set; }
        public double AverageRating { get; set; }
        public long Price { get; set; }
        public long? SalePrice { get; set; }
        public double DiscountPercent { get; set; }
        public string? Tag { get; set; }
        public  StockStatus StockStatus { get; set; }
        public string? Description { get; set; }
        public List<ProductFeatureDto>? Features { get; set; }
        public List<ProductVariableDto>? Variables { get; set; }
        public List<string>? Images { get; set; }
        public List<ProductReviewDto>? LatestReviews { get; set; }
        public List<ProductRelatedDto>? RelatedProducts { get; set; }
        public SeoDto? Seo { get; set; }
    }


    public class ProductsRaitingAndReviewsCountDto
    {
        public Guid ProductId { get; set; }
        public double Raiting { get; set; }
        public int ReviewsCount { get; set; }
    }


    public class ProductFeatureDto
    {
        public required string FeatureName { get; set; }
        public required Guid OptionId { get; set; }
        public string? OptionName { get; set; }
    }

    public class ProductVariableDto
    {
        public required string OptionName { get; set; }
        public long? Price { get; set; }
        public long? SalePrice { get; set; }
        public int StockQuantity { get; set; }
        public StockStatus StockStatus { get; set; }
        public double? Weight { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public string? Description { get; set; }
    }

    public class ProductReviewDto
    {
        public required string Title { get; set; }
        public required string ReviewText { get; set; }
        public int Rating { get; set; }
        public required string CreatedAt { get; set; }
    }

    public class ProductRelatedDto
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
    }

    public class SeoDto
    {
        public string? SeoTitle { get; set; }
        public string? MetaDescription { get; set; }
        public bool IsIndexed { get; set; }
        public bool IsFollowed { get; set; }
        public string? CanonicalUrl { get; set; }
    }
}
