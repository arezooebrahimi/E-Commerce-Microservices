using Auth.Protos;
using Grpc.Net.Client;

namespace Basket.Services.Grpc
{
    public class GrpcAuthClient
    {
        private readonly GrpcChannel _channel;
        private readonly AuthService.AuthServiceClient _client;

        public GrpcAuthClient(IConfiguration configuration)
        {
            var authServiceUrl = configuration["AuthService:GrpcUrl"];
            _channel = GrpcChannel.ForAddress(authServiceUrl!);
            _client = new AuthService.AuthServiceClient(_channel);
        }


        public async Task<(bool IsValid, string? UserId, string? ErrorMessage)> ValidateTokenAsync(string token)
        {
            var request = new TokenRequest { Token = token };
            var response = await _client.ValidateTokenAsync(request);

            return (response.IsValid, response.UserId, response.ErrorMessage);
        }
    }
} 