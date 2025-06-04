using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Dtos.Catalog.Category
{
    public class CategoryHomePageResponse
    {

        [JsonIgnore]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string FilePath { get; set; }
        public string? AltText { get; set; }

        [JsonIgnore]
        public ICollection<CategoryMedia>? Medias { get; set; }
    }
}
