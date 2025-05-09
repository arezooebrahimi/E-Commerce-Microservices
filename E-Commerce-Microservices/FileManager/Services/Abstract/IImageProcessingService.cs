using Common.Entities.FileManager;

namespace FileManager.Services.Abstract
{
    public interface IImageProcessingService
    {
        Task<List<MediaFormat>> GenerateFormatsAsync(Stream imageStream,string fileName, string uploadFolderPath, string ext);
        Task<(string fileName, string filePath)> ConvertFormatAsync(Stream imageStream, string fileNameWithoutExt, string outputFolderPath, string ext = "webp");
    }
}
