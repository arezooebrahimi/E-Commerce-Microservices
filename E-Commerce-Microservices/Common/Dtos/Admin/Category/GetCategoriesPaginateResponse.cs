
namespace Common.Dtos.Admin.Category;

public class GetCategoriesPaginateResponse
{
    public required IEnumerable<GetCategoriesPaginateDto> Categories { get; set; }
    public int Total { get; set; }
}

public class GetCategoriesPaginateDto
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? ParentName { get; set; }
}