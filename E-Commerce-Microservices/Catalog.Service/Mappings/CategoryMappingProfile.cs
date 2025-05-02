using AutoMapper;
using Common.Dtos.Catalog.Category;
using Common.Entities;

namespace Catalog.API.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryHomePageResponse>();
    }
} 