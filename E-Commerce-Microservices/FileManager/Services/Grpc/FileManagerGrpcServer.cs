using FileManager.Grpc;
using FileManager.Services.Abstract;
using Grpc.Core;

namespace FileManager.Services.Grpc
{
    public class FileManagerGrpcServer: FileService.FileServiceBase
    {
        private readonly IMediaService _mediaService;
        public FileManagerGrpcServer(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }
       
        public override async Task<UploadFilesReply> UploadFiles(UploadFilesRequest request, ServerCallContext context)
        {
            var formFiles = new List<IFormFile>();

            foreach (var file in request.Files)
            {
                var bytes = file.FileData.ToByteArray();
                var stream = new MemoryStream(bytes);
                var formFile = new FormFile(stream, 0, bytes.Length, file.FileName, file.FileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = file.ContentType
                };

                formFiles.Add(formFile);
            }

            var files = await _mediaService.UploadFilesAsync(formFiles);
            var reply = new UploadFilesReply();
            reply.MediaDocuments.AddRange(files.Select(file => new MediaDocument
            {
                Id = file.Id ?? "",
                FileName = file.FileName,
                FilePath = file.FilePath,
                MimeType = file.MimeType,
                Size = file.Size,
                CreatedAt = file.CreatedAt.ToString(),
                Formats = {
                    file.Formats?.Select(format => new MediaFormat
                    {
                        FileName = format.FileName,
                        FilePath = format.FilePath,
                        Format = format.Format,
                        Ext = format.Ext,
                        Width = format.Width,
                        Height = format.Height
                    }) ?? Enumerable.Empty<MediaFormat>()
                }
            }));

            return reply;
        }
    }
}
