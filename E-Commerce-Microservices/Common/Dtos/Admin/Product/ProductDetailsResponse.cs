using Common.Entities.Enums;

namespace Common.Dtos.Admin.Product
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
        public required List<ProductImageDto> Images { get; set; }
        public List<ProductReviewDto>? LatestReviews { get; set; }
        public List<ProductsResponse>? RelatedProducts { get; set; }
        public List<ProductsCategorieDto>? Categories { get; set; }
        public List<ProductsCategorieDto>? Tags { get; set; }
        public SeoDto? Seo { get; set; }
    }


    public class ProductsCategorieDto
    {
        public required string Slug { get; set; }
        public required string Name { get; set; }
    }


    public class ProductsRaitingAndReviewsCountDto
    {
        public Guid ProductId { get; set; }
        public double Raiting { get; set; }
        public int ReviewsCount { get; set; }
    }


    public class ProductImageDto
    {
        public bool IsPrimary { get; set; }
        public string? Title { get; set; }
        public string? AltText { get; set; }
        public List<ProductImageFormatDto>? Formats { get; set; }
    }


    public class ProductImageFormatDto
    {
        public required string FilePath { get; set; }
        public required string Format { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }


    public class ProductFeatureDto
    {
        public required string FeatureName { get; set; }
        public required Guid OptionId { get; set; }
        public string? OptionName { get; set; }
    }

    public class ProductVariableDto
    {
        public required Guid OptionId { get; set; }
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
        public required string Name { get; set; }
        public required string Title { get; set; }
        public required string ReviewText { get; set; }
        public int Rating { get; set; }
        public required string CreatedAt { get; set; }
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
