using AutoMapper;
using Common.Dtos.Admin.Category;
using Common.Dtos.Catalog.Category;
using Common.Entities;

namespace Admin.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, GetCategoriesPaginateDto>();
        CreateMap<CreateCategoryRequest, Category>().
            ForMember(dest => dest.Medias, opt => opt.Ignore()).
            ForMember(dest => dest.ImageIdOnHomePage, opt => opt.Ignore());

    }
} 