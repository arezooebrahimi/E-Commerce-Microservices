

namespace Common.Dtos.Admin.FeatureOption
{
    public class GetFeatureOptionsPaginateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Slug { get; set; }
        public int Order { get; set; }
        public string? FeatureName { get; set; }
    }
}
