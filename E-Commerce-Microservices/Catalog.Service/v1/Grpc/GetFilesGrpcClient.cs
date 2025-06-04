using GetFiles.Grpc;
using Grpc.Net.Client;

namespace Catalog.Service.v1.Grpc
{
    public class GetFilesGrpcClient
    {
        private readonly GetFilesService.GetFilesServiceClient _client;

        public GetFilesGrpcClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:44344");
            _client = new GetFilesService.GetFilesServiceClient(channel);
        }

        public async Task<List<MediaDocument>> GetFilesByIds(List<string> mediaIds)
        {
            var request = new GetFilesByIdsRequest();
            request.Ids.AddRange(mediaIds);
            var response = await _client.GetFilesByIdsAsync(request);
            return response.MediaDocuments.ToList();
        }
    }
}
