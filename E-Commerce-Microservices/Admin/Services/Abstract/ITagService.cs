using Common.Dtos.Admin.Tag;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Abstract
{
    public interface ITagService
    {
        Task<PagedResponse<GetTagsPaginateDto>> GetAllPaginateAsync(PagedRequest request);
        Task<Tag> AddAsync(CreateTagRequest tag);
        Task<Tag?> UpdateAsync(CreateTagRequest tag);
        Task DeleteAsync(IEnumerable<Tag> tags);
        Task<Tag?> GetByIdAsync(Guid id);
        Task<IEnumerable<Tag>> GetAllByIdsAsync(List<Guid> ids);
    }
}
