using FileManager.API.WebFramework.Api;
using FileManager.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers
{
    [Route("file_manager/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IArvanFileService _arvanFileService;
        private readonly IFileService _fileService;
        private readonly string _uploadFolderPath;

        public MediaController(IArvanFileService arvanFileService,IFileService fileService)
        {
            _arvanFileService = arvanFileService;
            _fileService = fileService;
            _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
        }


        [HttpPost("upload")]
        public async Task<ApiResult<string>> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var fileName = _fileService.GenerateNewFileName(file);
            var filePath = Path.Combine(_uploadFolderPath, fileName);
            await _fileService.UploadFileAsync(file, fileName, filePath);

            try
            {
                var uploadedUrl = await _arvanFileService.UploadFileAsync(fileName, filePath, file.ContentType);
                return Ok(uploadedUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                _fileService.DeleteFile(fileName);
            }
        }


        [HttpDelete("{fileName}")]
        public async Task<ApiResult<string>> DeleteFile(string fileName)
        {
            try
            {
                await _arvanFileService.DeleteFileAsync(fileName);
                return Ok("File deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"File deletion failed: {ex.Message}");
            }
        }
    }
}
