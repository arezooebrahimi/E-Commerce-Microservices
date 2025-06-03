namespace Common.Dtos.Admin.ProductReview
{
    public class GetProductReviewsPaginateDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public required bool IsUser { get; set; }
        public required string Title { get; set; }
        public required string ReviewText { get; set; }
        public int Rating { get; set; }
        public bool IsApproved { get; set; }
        public required string CreatedAt { get; set; }
        public required string ProductName { get; set; }
    }
}
