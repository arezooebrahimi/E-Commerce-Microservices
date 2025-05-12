using Admin.Dtos.Common;
using Admin.Repositories.Abstract;
using Admin.Services.Abstract;
using AutoMapper;
using Common.Dtos.Catalog.Feature;
using Common.Dtos.Common;
using Common.Entities;
using Common.Exceptions;

namespace Admin.Services.Concrete
{
    public class FeatureService : IFeatureService
    {
        private readonly IRepository<Feature> _featureRepository;
        private readonly IMapper _mapper;

        public FeatureService(IRepository<Feature> featureRepository, IMapper mapper)
        {
            _mapper = mapper;
            _featureRepository = featureRepository;
        }

        public async Task<PagedResponse<GetFeaturesPaginateDto>> GetAllPaginateAsync(PagedRequest request)
        {
            request.Includes = new List<string>() { "Options" };
            var (features, total) = await _featureRepository.GetAllPaginateAsync(request);

            var response = new PagedResponse<GetFeaturesPaginateDto>
            {
                Total = total,
                Items = features.Select(feature => new GetFeaturesPaginateDto
                {
                    Name = feature.Name,
                    Slug = feature.Slug,
                    OptionsName = feature.Options?.Select(o => o.Name).ToList()
                }).ToList()
            };

            return response;
        }

        public async Task<Feature?> GetByIdAsync(Guid id)
        {
            return await _featureRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Feature>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await _featureRepository.GetAllByIdsAsync(ids);
        }

        public async Task<Feature> AddAsync(CreateFeatureRequest request)
        {
            bool isDuplicate = await _featureRepository.IsSlugDuplicateAsync(request.Slug);
            if (isDuplicate)
                throw new AppException($"Slug '{request.Slug}' is already in use.");

            var entity = await _featureRepository.AddAsync(_mapper.Map<Feature>(request));
            await _featureRepository.SaveChangesAsync();
            return entity;
        }

        public async Task<Feature?> UpdateAsync(CreateFeatureRequest request)
        {
            Feature? entity = null;
            if (request.Id != null)
            {
                entity = await _featureRepository.GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    await _featureRepository.SaveChangesAsync();
                }
            }
            return entity;
        }

        public async Task DeleteAsync(IEnumerable<Feature> features)
        {
            _featureRepository.DeleteRange(features);
            await _featureRepository.SaveChangesAsync();
        }
    }
}
