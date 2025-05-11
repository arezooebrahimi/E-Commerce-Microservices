using Common.Entities.Abstract;
using Common.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Product : SeoBaseEntity, IEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public Guid? BrandId { get; set; }
        public short Type { get; set; }
        public long Price { get; set; }
        public long? SalePrice { get; set; }
        public DateTime? DateOnSaleFrom { get; set; }
        public DateTime? DateOnSaleTo { get; set; }
        public bool OpenedComments { get; set; }
        public int StockQuantity { get; set; }
        public bool ManageStock { get; set; }
        public StockStatus StockStatus { get; set; }
        public Status Status { get; set; }
        public bool IsSuggested { get; set; }
        public bool DisplayOnHomePage { get; set; }
        public int OrderOnHomePage { get; set; }
        public string? Tag { get; set; }

        // Navigation properties
        [ForeignKey("BrandId")]
        public virtual Brand? Brand {  get; set; } 
        public required ICollection<ProductTag> Tags { get; set; }
        public required ICollection<ProductCategory> Categories { get; set; }
        public required ICollection<ProductFeature> Features { get; set; }
        public required ICollection<ProductMedia>? Medias { get; set; }
        public required ICollection<ProductVariable> Variables { get; set; }
        public required ICollection<ProductReview> Reviews { get; set; }
        public required ICollection<ProductRelated> RelatedProducts { get; set; }
        public required ICollection<ProductRelated> LinkedProducts { get; set; }
        public required ICollection<ProductQuestion>? Questions { get; set; }
    }
}
    