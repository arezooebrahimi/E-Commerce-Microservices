using Common.Dtos.Common;
using Common.Dtos.FileManager;
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


        [HttpPost]
        public async Task<ApiResult<PagedResponse<MediaDocument>>> Get(GetMediasRequest req)
        {
            var medias = await _mediaService.GetAllAsync(req);
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



        [HttpDelete]
        public async Task<ApiResult> DeleteFile(List<string> ids)
        {
            var response = await _mediaService.DeleteAsync(ids);
            if (response)
                return Ok();
            return BadRequest();
        }
    }
}
