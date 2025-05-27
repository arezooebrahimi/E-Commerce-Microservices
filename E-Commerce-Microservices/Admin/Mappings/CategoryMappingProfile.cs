using AutoMapper;
using Common.Dtos.Admin.Category;
using Common.Dtos.Catalog.Brand;
using Common.Dtos.Catalog.Category;
using Common.Dtos.Catalog.Feature;
using Common.Dtos.Catalog.FeatureOption;
using Common.Dtos.Catalog.Product;
using Common.Dtos.Catalog.Tag;
using Common.Entities;

namespace Admin.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, GetCategoriesPaginateDto>();
        CreateMap<Tag, GetTagsPaginateDto>();
        CreateMap<Brand, GetBrandsPaginateDto>();
        CreateMap<Product, GetProductsPaginateDto>();
        CreateMap<Feature, GetFeaturesPaginateDto>();
        CreateMap<FeatureOption, GetFeatureOptionsPaginateDto>();

        CreateMap<CreateCategoryRequest, Category>();


        CreateMap<CreateTagRequest, Tag>().
            ForMember(dest => dest.Medias, opt => opt.Ignore());

        CreateMap<CreateProductRequest, Product>().
            ForMember(dest => dest.Medias, opt => opt.Ignore());

        CreateMap<CreateFeatureRequest, Feature>();
        CreateMap<CreateFeatureOptionRequest, FeatureOption>();
        CreateMap<CreateBrandRequest, Brand>();
    }
} 