using Common.Entities.Abstract;

namespace Common.Entities
{
    public class Tag : SeoBaseEntity , IEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Slug { get; set; }
        public string? FeaturingImageName { get; set; }
        public int Order { get; set; }
        
        // Navigation properties
        public ICollection<ProductTag>? Products { get; set; }
        public ICollection<TagMedia>? Medias { get; set; }
    }
} 