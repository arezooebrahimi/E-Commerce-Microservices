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

        public async Task<List<string>> UploadFilesAsync(List<IFormFile> files)
        {
            var uploadedIds = new List<string>();
            foreach (var file in files)
            {
                var fileName = _fileService.GenerateNewFileName(file);
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                List<MediaFormat>? filesFormated = null;
                using var stream = file.OpenReadStream();
                var filePath = Path.Combine(_uploadFolderPath, fileName);
                bool isImage = file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);

                try
                {
                    if (isImage)
                    {
                        filesFormated = await _imageProcessingService.GenerateFormatsAsync(stream, fileNameWithoutExt, _uploadFolderPath, ".webp");
                        (fileName, filePath) = await _imageProcessingService.ConvertFormatAsync(stream, fileNameWithoutExt, _uploadFolderPath, ".webp");

                        for (int i = 0; i < filesFormated.Count; i++)
                        {
                            var item = filesFormated[i];
                            var uploadedPath = await _arvanFileService.UploadFileAsync(item.FileName, item.FilePath);
                            item.FilePath = uploadedPath;

                            _fileService.DeleteFile(item.FileName);
                        }
                    }
                    else
                        await _fileService.UploadFileAsync(file, fileName, filePath);

                    filePath = await _arvanFileService.UploadFileAsync(fileName, filePath);

                    var mediaDoc = new MediaDocument()
                    {
                        FileName = fileName,
                        FilePath = filePath,
                        MimeType = file.ContentType,
                        Size = stream.Length,
                        Formats = filesFormated,
                        CreatedAt = DateTime.Now,
                    };

                    await CreateAsync(mediaDoc);

                    uploadedIds.Add(mediaDoc.Id.ToString());
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

            return uploadedIds;
        }

        public Task<List<MediaDocument>> GetAllAsync()
        {
            return _repository.GetAllAsync();
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

        public async Task<bool> DeleteAsync(string id)
        {
            try
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
                    await _repository.DeleteAsync(id);
                }
                else
                {
                    _logger.LogWarning($"File with id = {id} does not exist");
                }
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
