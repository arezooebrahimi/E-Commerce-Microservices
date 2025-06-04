using FileManager.Services.Abstract;
using GetFiles.Grpc;
using Grpc.Core;

namespace FileManager.Services.Grpc
{
    public class GetFilesGrpcService : GetFilesService.GetFilesServiceBase
    {
        private readonly IMediaService _mediaService;

        public GetFilesGrpcService(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public override async Task<GetFilesByIdsResponse> GetFilesByIds(GetFilesByIdsRequest request, ServerCallContext context)
        {
            var mediaList = await _mediaService.GetByIdsAsync(request.Ids.ToList());

            var response = new GetFilesByIdsResponse();

            foreach (var media in mediaList)
            {
                var mediaDoc = new MediaDocument
                {
                    Id = media.Id ?? "",
                    FileName = media.FileName,
                    FilePath = media.FilePath,
                    MimeType = media.MimeType,
                    Size = media.Size,
                    CreatedAt = media.CreatedAt.ToString("o")
                };

                if (media.Formats != null)
                {
                    mediaDoc.Formats.AddRange(media.Formats.Select(f => new MediaFormat
                    {
                        FileName = f.FileName,
                        FilePath = f.FilePath,
                        Format = f.Format,
                        Ext = f.Ext,
                        Width = f.Width,
                        Height = f.Height
                    }));
                }

                response.MediaDocuments.Add(mediaDoc);
            }

            return response;
        }
    }
}
