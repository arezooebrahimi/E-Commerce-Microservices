using Admin.Repositories.Abstract;
using Admin.Services.Abstract;
using AutoMapper;
using Common.Dtos.Catalog.Brand;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetBrandsPaginateDto>> GetAllPaginateAsync(PagedRequest request)
        {
            var (brands, total) = await _brandRepository.GetAllPaginateAsync(request);

            var response = new PagedResponse<GetBrandsPaginateDto>
            {
                Total = total,
                Items = brands.Select(brand => new GetBrandsPaginateDto
                {
                    Name = brand.Name,
                    Description = brand.Description,
                    IsActive = brand.IsActive
                }).ToList()
            };

            return response;
        }

        public async Task<Brand?> GetByIdAsync(Guid id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await _brandRepository.GetAllByIdsAsync(ids);
        }

        public async Task<Brand> AddAsync(CreateBrandRequest request)
        {
            var entity = await _brandRepository.AddAsync(_mapper.Map<Brand>(request));
            await _brandRepository.SaveChangesAsync();
            return entity;
        }

        public async Task<Brand?> UpdateAsync(CreateBrandRequest request)
        {
            Brand? entity = null;
            if (request.Id != null)
            {
                entity = await _brandRepository.GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    await _brandRepository.SaveChangesAsync();
                }
            }
            return entity;
        }

        public async Task DeleteAsync(IEnumerable<Brand> brands)
        {
            _brandRepository.DeleteRange(brands);
            await _brandRepository.SaveChangesAsync();
        }
    }
}
