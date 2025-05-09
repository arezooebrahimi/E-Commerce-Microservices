using Common.Entities.FileManager;
using Common.WebFramework.Api;
using FileManager.Services.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace FileManager.Controllers
{
    [Route("file_manager/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }


        [HttpPost("upload")]
        public async Task<ApiResult<List<string>>> UploadFile([FromForm] List<IFormFile> files)
        {
            if (files.Count == 0)
                return BadRequest("No file uploaded.");

           var response = await _mediaService.UploadFilesAsync(files);
            return Ok(response);
        }


        [HttpGet]
        public async Task<ApiResult<List<MediaDocument>>> Get()
        {
            var medias = await _mediaService.GetAllAsync();
            return Ok(medias);
        }


        [HttpGet("{id}")]
        public async Task<ApiResult<MediaDocument>> GetById(string id)
        {
            var media = await _mediaService.GetByIdAsync(id);
            if (media == null)
                return NotFound();

            return Ok(media);
        }


        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteFile(string id)
        {
            var response = await _mediaService.DeleteAsync(id);
            if(response)
                return Ok();
            return BadRequest();
        }
    }
}
