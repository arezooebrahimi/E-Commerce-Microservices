namespace FileManager.Services.Abstract
{
    public interface IFileService
    {
        Task UploadFileAsync(IFormFile file,string fileName,string filePath);
        bool DeleteFile(string fileName);
        string GenerateNewFileName(IFormFile file);
    }
}
