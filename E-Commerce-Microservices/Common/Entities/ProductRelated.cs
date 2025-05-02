namespace Common.Entities
{
    public class ProductRelated
    {
        public Guid ProductId { get; set; }
        public Guid RelatedProductId { get; set; }
        public int Order { get; set; }
        public Product? Product { get; set; }
        public Product? RelatedProduct { get; set; }
    }
} 