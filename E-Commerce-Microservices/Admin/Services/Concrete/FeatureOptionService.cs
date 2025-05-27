using Admin.Repositories.Abstract;
using Admin.Services.Abstract;
using AutoMapper;
using Common.Dtos.Catalog.FeatureOption;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Concrete
{
    public class FeatureOptionService : IFeatureOptionService
    {
        private readonly IRepository<FeatureOption> _optionRepository;
        private readonly IMapper _mapper;

        public FeatureOptionService(IRepository<FeatureOption> optionRepository, IMapper mapper)
        {
            _optionRepository = optionRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetFeatureOptionsPaginateDto>> GetAllPaginateAsync(PagedRequest request)
        {
            request.Includes = new List<string>() { "Feature" };
            var (options, total) = await _optionRepository.GetAllPaginateAsync(request);

            var response = new PagedResponse<GetFeatureOptionsPaginateDto>
            {
                Total = total,
                Items = options.Select(option => new GetFeatureOptionsPaginateDto
                {
                    Name = option.Name,
                    Slug = option.Slug,
                    FeatureName = option.Feature?.Name
                }).ToList()
            };

            return response;
        }

        public async Task<FeatureOption?> GetByIdAsync(Guid id)
        {
            return await _optionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<FeatureOption>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await _optionRepository.GetAllByIdsAsync(ids);
        }

        public async Task<FeatureOption> AddAsync(CreateFeatureOptionRequest request)
        {
            var entity = await _optionRepository.AddAsync(_mapper.Map<FeatureOption>(request));
            await _optionRepository.SaveChangesAsync();
            return entity;
        }

        public async Task<FeatureOption?> UpdateAsync(CreateFeatureOptionRequest request)
        {
            FeatureOption? entity = null;
            if (request.Id != null)
            {
                entity = await _optionRepository.GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    await _optionRepository.SaveChangesAsync();
                }
            }
            return entity;
        }

        public async Task DeleteAsync(IEnumerable<FeatureOption> options)
        {
            _optionRepository.DeleteRange(options);
            await _optionRepository.SaveChangesAsync();
        }
    }
}
