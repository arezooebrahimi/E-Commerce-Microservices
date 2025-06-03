using Common.Entities.Abstract;
using System.Collections.Generic;

namespace Common.Entities
{
    public class Feature : BaseEntity, IEntity, ISlugEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public bool IsFilter { get; set; }

        // Navigation properties
        public required ICollection<ProductFeature> Products { get; set; }
        public required ICollection<FeatureOption> Options { get; set; }
    }
} 