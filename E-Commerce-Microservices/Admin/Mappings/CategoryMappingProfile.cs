using AutoMapper;
using Common.Dtos.Admin.Category;
using Common.Entities;

namespace Admin.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, GetCategoriesPaginateDto>();
    }
} 