namespace FileManager.Services.Abstract
{
    public interface IArvanFileService
    {
        Task<string> UploadFileAsync(string fileName, string filePath,string contentType);
        Task DeleteFileAsync(string fileName);
    }
}
