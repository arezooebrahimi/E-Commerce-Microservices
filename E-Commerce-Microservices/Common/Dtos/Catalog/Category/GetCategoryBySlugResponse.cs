using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Catalog.Category
{
    public class GetCategoryBySlugResponse
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Slug { get; set; }
        public string? SeoTitle { get; set; }
        public int SeoTitleLength { get; set; }
        public string? MetaDescription { get; set; }
        public int MetaDescriptionLength { get; set; }
        public bool IsIndexed { get; set; }
        public bool IsFollowed { get; set; }
        public string? CanonicalUrl { get; set; }
        public List<string> ChildsSlug { get; set; }= new List<string>();
    }
}
