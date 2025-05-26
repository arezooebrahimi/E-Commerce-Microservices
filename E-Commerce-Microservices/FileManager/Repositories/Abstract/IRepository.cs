using Common.Dtos.Common;
using Common.Dtos.FileManager;
using Common.Entities.Abstract;

namespace FileManager.Repositories.Abstract
{
    public interface IRepository<TDocument> where TDocument : class,ICreatedAtEntity, IFilePathEntity
    {
        Task<PagedResponse<TDocument>> GetAllAsync(GetMediasRequest req);
        Task<TDocument?> GetByIdAsync(string id);
        Task CreateAsync(TDocument document);
        Task<bool> UpdateAsync(string id, TDocument document);
        Task<bool> DeleteAsync(List<string> ids);
        Task<bool> ExistsByFilePathAsync(string filePath);
    }
}
