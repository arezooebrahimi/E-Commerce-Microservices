using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class TagMedia
    {
        public Guid TagId { get; set; }
        public string? MediaId { get; set; }
        public string? AltText { get; set; }
        public string? Caption { get; set; }
        public string? Title { get; set; }
        public bool IsPrimary { get; set; }

        // Navigation properties
        [ForeignKey("TagId")]
        public Tag? Tag { get; set; }
    }
} 