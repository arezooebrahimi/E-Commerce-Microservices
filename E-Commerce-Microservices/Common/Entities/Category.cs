using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Category : SeoBaseEntity
    {
        public Guid? ParentId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Slug { get; set; }
        public int Order { get; set; }
        public bool DisplayOnHomePage { get; set; }
        public int OrderOnHomePage { get; set; }
        public string? ImageUrlOnHomePage { get; set; }

        // Navigation properties
        public  Category? Parent { get; set; }
        public required ICollection<Category> SubCategories { get; set; }
        public required ICollection<ProductCategory> Products { get; set; }
        public required ICollection<CategoryMedia> Medias { get; set; }
    }
}
