using Common.Entities.FileManager;

namespace FileManager.Services.Abstract
{
    public interface IMediaService
    {
        Task<List<string>> UploadFilesAsync(List<IFormFile> files);
        Task<List<MediaDocument>> GetAllAsync();
        Task<MediaDocument?> GetByIdAsync(string id);
        Task CreateAsync(MediaDocument document);
        Task<bool> UpdateAsync(string id, MediaDocument document);
        Task<bool> DeleteAsync(string id);
    }
}
