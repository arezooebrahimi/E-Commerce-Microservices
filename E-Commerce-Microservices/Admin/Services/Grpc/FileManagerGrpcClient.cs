using FileManager.Grpc;
using Grpc.Net.Client;

namespace Admin.Services.Grpc
{
    public class FileManagerGrpcClient
    {
        private readonly FileService.FileServiceClient _client;

        public FileManagerGrpcClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:44344");
            _client = new FileService.FileServiceClient(channel);
        }

        public async Task<List<string>> UploadFilesAsync(List<IFormFile> files)
        {
            var request = new UploadFilesRequest();

            foreach (var file in files)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var fileItem = new FileItem
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FileData = Google.Protobuf.ByteString.CopyFrom(ms.ToArray())
                };
                request.Files.Add(fileItem);
            }

            var response = await _client.UploadFilesAsync(request);

            return response.FileIds.ToList();
        }
    }
}
