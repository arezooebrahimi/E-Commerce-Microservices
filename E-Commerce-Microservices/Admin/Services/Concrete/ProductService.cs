using Admin.Repositories.Abstract;
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

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
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

            var entity = await _productRepository.AddAsync(_mapper.Map<Product>(request));
            await _productRepository.SaveChangesAsync();
            return entity;
        }

        public async Task<Product?> UpdateAsync(CreateProductRequest request)
        {
            Product? entity = null;
            if (request.Id != null)
            {
                entity = await _productRepository.GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    _mapper.Map(request, entity);
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
