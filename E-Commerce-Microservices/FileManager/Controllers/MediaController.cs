using FileManager.API.WebFramework.Api;
using FileManager.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers
{
    [Route("file_manager/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IFileService _fileService;

        public MediaController(IFileService fileService)
        {
            _fileService = fileService;
        }


        [HttpPost("upload")]
        public async Task<ApiResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileUrl = await _fileService.UploadFileAsync(file);
            return Ok();
        }


        [HttpDelete("{fileName}")]
        public ApiResult<string> DeleteFile(string fileName)
        {
            try
            {
                _fileService.DeleteFile(fileName);
                return Ok("File deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"File deletion failed: {ex.Message}");
            }
        }
    }
}
