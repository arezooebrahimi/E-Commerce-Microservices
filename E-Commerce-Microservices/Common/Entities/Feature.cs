using System.Collections.Generic;

namespace Common.Entities
{
    public class Feature : BaseEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Type { get; set; }

        // Navigation properties
        public required ICollection<ProductFeature> Products { get; set; }
        public required ICollection<FeatureOption> Options { get; set; }
    }
} 