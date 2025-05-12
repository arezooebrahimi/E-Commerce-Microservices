using System;
using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class CategoryMedia
    {
        public Guid CategoryId { get; set; }
        public string? MediaId { get; set; }
        public string? AltText { get; set; }
        public string? Caption { get; set; }
        public string? Title { get; set; }
        public bool IsPrimary { get; set; }

        // Navigation properties

        [JsonIgnore]
        public Category? Category { get; set; }
    }
} 