using Common.Dtos.Catalog.FeatureOption;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Abstract
{
    public interface IFeatureOptionService
    {
        Task<PagedResponse<GetFeatureOptionsPaginateDto>> GetAllPaginateAsync(PagedRequest request);
        Task<FeatureOption> AddAsync(CreateFeatureOptionRequest request);
        Task<FeatureOption?> UpdateAsync(CreateFeatureOptionRequest request);
        Task DeleteAsync(IEnumerable<FeatureOption> options);
        Task<FeatureOption?> GetByIdAsync(Guid id);
        Task<IEnumerable<FeatureOption>> GetAllByIdsAsync(List<Guid> ids);
    }
}
