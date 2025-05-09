namespace FileManager.Services.Abstract
{
    public interface IArvanFileService
    {
        Task<string> UploadFileAsync(string fileName, string filePath);
        Task DeleteFileAsync(string fileName);
    }
}
