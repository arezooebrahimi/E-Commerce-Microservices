using AutoMapper;
using Common.Dtos.Admin.Brand;
using Common.Dtos.Admin.Category;
using Common.Dtos.Admin.Feature;
using Common.Dtos.Admin.FeatureOption;
using Common.Dtos.Admin.Product;
using Common.Dtos.Admin.Tag;
using Common.Dtos.Catalog.Category;
using Common.Entities;

namespace Admin.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
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