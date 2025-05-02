namespace Common.Entities
{
    public abstract class SeoBaseEntity : BaseEntity
    {
            public string? SeoTitle { get; set; }
            public int SeoTitleLength { get; set; }
            public string? MetaDescription { get; set; }
            public int MetaDescriptionLength { get; set; }
            public bool IsIndexed { get; set; }
            public bool IsFollowed { get; set; }
            public string? CanonicalUrl { get; set; }
        
    }
} 