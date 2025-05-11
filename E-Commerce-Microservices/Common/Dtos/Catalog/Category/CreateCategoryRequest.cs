

namespace Common.Dtos.Catalog.Category
{
    public class CreateCategoryRequest
    {
        public Guid? Id { get; set; }
        public Guid? ParentId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Slug { get; set; }
        public int Order { get; set; }
        public bool DisplayOnHomePage { get; set; }
        public int OrderOnHomePage { get; set; }
    }
}
