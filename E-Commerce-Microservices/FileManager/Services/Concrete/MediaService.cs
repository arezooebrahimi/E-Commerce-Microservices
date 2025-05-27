using Common.Dtos.Common;
using Common.Dtos.FileManager;
using Common.Entities.FileManager;
using FileManager.Models.Mongodb;
using FileManager.Repositories.Abstract;
using FileManager.Repositories.Concrete;
using FileManager.Services.Abstract;
using Microsoft.Extensions.Options;


namespace FileManager.Services.Concrete
{
    public class MediaService:IMediaService
    {
        private readonly ILogger<MediaService> _logger;
        private readonly IRepository<MediaDocument> _repository;
        private readonly IArvanFileService _arvanFileService;
        private readonly IFileService _fileService;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly string _uploadFolderPath;

        public MediaService(IOptions<MongoDbSettings> mongoSettings, ILogger<MediaService> logger, IArvanFileService arvanFileService, IFileService fileService, IImageProcessingService imageProcessingService)
        {
            _repository = new Repository<MediaDocument>(mongoSettings, "media");
            _arvanFileService = arvanFileService;
            _fileService = fileService;
            _imageProcessingService = imageProcessingService;
            _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            _logger = logger;
        }

        public async Task<List<MediaDocument>> UploadFilesAsync(List<IFormFile> files)
        {
            var uploaded = new List<MediaDocument>();
            foreach (var file in files)
            {
                string? formatFileKey = null;
                bool isImage = file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
                var fileName = file.FileName;
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                var mimeType = file.ContentType;

                string folderName = DateTime.Now.ToString("MM-yyyy");
                string fileKey = $"{folderName}/{fileName}";

                if (isImage && fileNameWithoutExt != "webp")
                {
                    fileKey = $"{folderName}/{fileNameWithoutExt}.webp";
                    mimeType = "image/webp";
                }

                if (await _repository.ExistsByFilePathAsync(fileKey))
                {
                    fileName = _fileService.GenerateNewFileName(file);
                    fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                    if (isImage && fileNameWithoutExt != "webp")
                        fileKey = $"{folderName}/{fileNameWithoutExt}.webp";
                    else
                        fileKey = $"{folderName}/{fileName}";
                }
;
                var filePath = Path.Combine(_uploadFolderPath, fileName);

                List<MediaFormat>? filesFormated = null;
                using var stream = file.OpenReadStream();

                try
                {
                    if (isImage)
                    {
                        filesFormated = await _imageProcessingService.GenerateFormatsAsync(stream, fileNameWithoutExt, _uploadFolderPath, ".webp");
                        (fileName, filePath) = await _imageProcessingService.ConvertFormatAsync(stream, fileNameWithoutExt, _uploadFolderPath, ".webp");

                        for (int i = 0; i < filesFormated.Count; i++)
                        {
                            var item = filesFormated[i];
                            formatFileKey = $"{folderName}/{item.FileName}";
                            await _arvanFileService.UploadFileAsync(formatFileKey, item.FilePath);
                            item.FilePath = formatFileKey;

                            _fileService.DeleteFile(item.FileName);
                        }
                    }
                    else
                        await _fileService.UploadFileAsync(file, fileName, filePath);

                    await _arvanFileService.UploadFileAsync(fileKey, filePath);

                    var mediaDoc = new MediaDocument()
                    {
                        FileName = fileName,
                        FilePath = fileKey,
                        MimeType = mimeType,
                        Size = stream.Length,
                        Formats = filesFormated,
                        CreatedAt = DateTime.Now,
                    };

                    await CreateAsync(mediaDoc);

                    uploaded.Add(mediaDoc);
                }

                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message}");
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    _fileService.DeleteFile(fileName);
                }
            }

            return uploaded;
        }

        public Task<PagedResponse<MediaDocument>> GetAllAsync(GetMediasRequest req)
        {
            return _repository.GetAllAsync(req);
        }

        public Task<MediaDocument?> GetByIdAsync(string id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task CreateAsync(MediaDocument document)
        {
            return _repository.CreateAsync(document);
        }

        public Task<bool> UpdateAsync(string id, MediaDocument document)
        {
            return _repository.UpdateAsync(id, document);
        }

        public async Task<bool> DeleteAsync(List<string> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var document = await _repository.GetByIdAsync(id);
                    if (document != null)
                    {
                        await _arvanFileService.DeleteFileAsync(document.FilePath);
                        if (document.Formats != null)
                        {
                            foreach (var format in document.Formats)
                            {
                                await _arvanFileService.DeleteFileAsync(format.FilePath);
                            }
                        }
                    }
                }

                await _repository.DeleteAsync(ids);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"File deletion failed: {ex.Message}");
                return false;
            }
        }
    }
}
