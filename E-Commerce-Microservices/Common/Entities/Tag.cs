using System.Collections.Generic;

namespace Common.Entities
{
    public class Tag : SeoBaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Slug { get; set; }
        public string? FeaturingImageName { get; set; }
        public int Order { get; set; }
        
        // Navigation properties
        public required ICollection<ProductTag> Products { get; set; }
        public required ICollection<TagMedia> Medias { get; set; }
    }
} 