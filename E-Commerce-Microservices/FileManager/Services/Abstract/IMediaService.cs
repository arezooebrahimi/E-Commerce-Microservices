using Common.Dtos.Common;
using Common.Dtos.FileManager;
using Common.Entities.FileManager;

namespace FileManager.Services.Abstract
{
    public interface IMediaService
    {
        Task<List<MediaDocument>> UploadFilesAsync(List<IFormFile> files);
        Task<PagedResponse<MediaDocument>> GetAllAsync(GetMediasRequest req);
        Task<MediaDocument?> GetByIdAsync(string id);
        Task CreateAsync(MediaDocument document);
        Task<bool> UpdateAsync(string id, MediaDocument document);
        Task<bool> DeleteAsync(List<string> ids);
        Task<List<MediaDocument>> GetByIdsAsync(List<string> ids);
    }
}
