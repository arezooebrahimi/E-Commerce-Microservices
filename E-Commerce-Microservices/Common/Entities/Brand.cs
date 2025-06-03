using Common.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Brand:IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? MediaId { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public required List<Product> Products { get; set; }
    }
} 