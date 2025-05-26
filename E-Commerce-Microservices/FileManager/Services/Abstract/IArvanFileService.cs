namespace FileManager.Services.Abstract
{
    public interface IArvanFileService
    {
        Task UploadFileAsync(string fileKey, string filePath);
        Task DeleteFileAsync(string fileName);
    }
}
