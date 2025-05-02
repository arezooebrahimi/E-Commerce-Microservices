using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class ProductQuestion
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
} 