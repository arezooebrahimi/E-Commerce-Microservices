using Common.Entities.Abstract;
using System.Collections.Generic;

namespace Common.Entities
{
    public class FeatureOption : BaseEntity, IEntity
    {
        public Guid FeatureId { get; set; }
        public required string Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }

        // Navigation properties
        public virtual Feature? Feature { get; set; }
        public required ICollection<ProductVariable> Variables { get; set; }
        public required ICollection<ProductFeature> DefaultProductOptions { get; set; }
    }
} 