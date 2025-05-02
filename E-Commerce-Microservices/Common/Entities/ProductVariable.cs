using Common.Entities.Enums;

namespace Common.Entities
{
    public class ProductVariable : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string? MediaId { get; set; }
        public Guid FeatureOptionId { get; set; }
        public long? Price { get; set; }
        public long? SalePrice { get; set; }
        public DateTime? DateOnSaleFrom { get; set; }
        public DateTime? DateOnSaleTo { get; set; }
        public string? ProductNumber { get; set; }
        public int StockQuantity { get; set; }
        public StockStatus StockStatus { get; set; }
        public double? Weight { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public string? Description { get; set; }
        public bool? ManageStock { get; set; }

        // Navigation properties
        public Product? Product { get; set; }
        public FeatureOption? FeatureOption { get; set; }
    }
} 