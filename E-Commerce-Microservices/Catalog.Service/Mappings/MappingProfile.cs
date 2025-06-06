using AutoMapper;
using Common.Dtos.Admin.Product;
using Common.Dtos.Catalog.Category;
using Common.Entities;
using GetFiles.Grpc;

namespace Catalog.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryHomePageResponse>();
        CreateMap<Category, GetCategoryBySlugResponse>();

    }
} 