using Common.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Common.Entities
{
    public class ProductReview:IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? UserId { get; set; }
        public Guid ProductId { get; set; }
        public required string Title { get; set; }
        public required string ReviewText { get; set; }
        public int Rating { get; set; }
        public bool IsApproved { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
} 