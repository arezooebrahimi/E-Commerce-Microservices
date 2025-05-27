using System.Text.Json.Serialization;

namespace Common.Dtos.Common
{
    public class PagedRequest
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 10;
        public SortOptions? Sort { get; set; }
        public Dictionary<string, FilterOptions>? Filters { get; set; }

        [JsonIgnore]
        public List<string>? Includes { get; set; }
    }
}
