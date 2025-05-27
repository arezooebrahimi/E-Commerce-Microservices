using Common.Dtos.Catalog.Feature;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Abstract
{
    public interface IFeatureService
    {
        Task<PagedResponse<GetFeaturesPaginateDto>> GetAllPaginateAsync(PagedRequest request);
        Task<Feature> AddAsync(CreateFeatureRequest feature);
        Task<Feature?> UpdateAsync(CreateFeatureRequest feature);
        Task DeleteAsync(IEnumerable<Feature> features);
        Task<Feature?> GetByIdAsync(Guid id);
        Task<IEnumerable<Feature>> GetAllByIdsAsync(List<Guid> ids);
    }
}
