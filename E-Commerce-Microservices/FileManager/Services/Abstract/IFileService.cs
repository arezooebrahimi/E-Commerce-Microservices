namespace FileManager.Services.Abstract
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        bool DeleteFile(string fileName);
    }
}
