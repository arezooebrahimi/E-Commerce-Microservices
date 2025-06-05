using System;
using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class ProductMedia
    {
        public Guid ProductId { get; set; }
        public string? MediaId { get; set; }
        public bool IsPrimary { get; set; }
        public string? AltText { get; set; }
        public string? Caption { get; set; }
        public string? Title { get; set; }
        public int Order { get; set; }

        // Navigation properties

        [JsonIgnore]
        public  Product? Product { get; set; }
    }
} 