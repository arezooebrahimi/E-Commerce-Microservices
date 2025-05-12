using Admin.Dtos.Common;
using Admin.Repositories.Abstract;
using Admin.Services.Abstract;
using AutoMapper;
using Common.Dtos.Catalog.Tag;
using Common.Dtos.Common;
using Common.Entities;
using Common.Exceptions;

namespace Admin.Services.Concrete
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public TagService(IRepository<Tag> tagRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<PagedResponse<GetTagsPaginateDto>> GetAllPaginateAsync(PagedRequest request)
        {
            var (tags, total) = await _tagRepository.GetAllPaginateAsync(request);
            var response = new PagedResponse<GetTagsPaginateDto>
            {
                Total = total,
                Items = _mapper.Map<List<GetTagsPaginateDto>>(tags)
            };

            return response;
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await _tagRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Tag>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await _tagRepository.GetAllByIdsAsync(ids);
        }

        public async Task<Tag> AddAsync(CreateTagRequest request)
        {
            bool isDuplicate = await _tagRepository.IsSlugDuplicateAsync(request.Slug);
            if (isDuplicate)
                throw new AppException($"Slug '{request.Slug}' is already in use.");
            var entity = await _tagRepository.AddAsync(_mapper.Map<Tag>(request));
            await _tagRepository.SaveChangesAsync();
            return entity;
        }

        public async Task<Tag?> UpdateAsync(CreateTagRequest request)
        {
            Tag? entity = null;
            if (request.Id != null)
            {
                entity = await _tagRepository.GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    await _tagRepository.SaveChangesAsync();
                }
            }
            return entity;
        }

        public async Task DeleteAsync(IEnumerable<Tag> tags)
        {
            _tagRepository.DeleteRange(tags);
            await _tagRepository.SaveChangesAsync();
        }
    }
}
