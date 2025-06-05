using Admin.Repositories.Abstract;
using Admin.Repositories.Concrete;
using Admin.Services.Abstract;
using AutoMapper;
using Common.Dtos.Admin.Product;
using Common.Dtos.Common;
using Common.Entities;
using Common.Exceptions;

namespace Admin.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductMediaRepository _productMediaRepository;

        public ProductService(IRepository<Product> productRepository, IMapper mapper, IProductMediaRepository productMediaRepository)
        {
            _productRepository = productRepository;
            _productMediaRepository = productMediaRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetProductsPaginateDto>> GetAllPaginateAsync(PagedRequest request)
        {
            request.Includes = new List<string>() { "Brand" };
            var (products, total) = await _productRepository.GetAllPaginateAsync(request);

            var productDto = _mapper.Map<List<GetProductsPaginateDto>>(products);
            var response = new PagedResponse<GetProductsPaginateDto>
            {
                Total = total,
                Items = productDto
            };

            return response;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await _productRepository.GetAllByIdsAsync(ids);
        }

        public async Task<Product> AddAsync(CreateProductRequest request)
        {
            bool isDuplicate = await _productRepository.IsSlugDuplicateAsync(request.Slug);
            if (isDuplicate)
                throw new AppException($"Slug '{request.Slug}' is already in use.");

            var newEntity = _mapper.Map<Product>(request);

            if (newEntity.SeoTitle != null)
                newEntity.SeoTitleLength = newEntity.SeoTitle.Length;
            if (newEntity.MetaDescription != null)
                newEntity.MetaDescriptionLength = newEntity.MetaDescription.Length;

            var entity = await _productRepository.AddAsync(newEntity);
            await _productRepository.SaveChangesAsync();
            return entity;
        }

        public async Task<Product?> UpdateAsync(CreateProductRequest request)
        {
            Product? entity = null;
            if (request.Id != null)
            {
                var medias = await _productMediaRepository.GetByProductIdAsync(request.Id);
                if (medias.Count() != 0)
                    _productMediaRepository.DeleteRange(medias);

                entity = await _productRepository.GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    if (request.SeoTitle != null)
                        entity.SeoTitleLength = request.SeoTitle.Length;
                    if (request.MetaDescription != null)
                        entity.MetaDescriptionLength = request.MetaDescription.Length;

                    await _productRepository.SaveChangesAsync();
                }
            }
            return entity;
        }

        public async Task DeleteAsync(IEnumerable<Product> products)
        {
            _productRepository.DeleteRange(products);
            await _productRepository.SaveChangesAsync();
        }
    }
}
