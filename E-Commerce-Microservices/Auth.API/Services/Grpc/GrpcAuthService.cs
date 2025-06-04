using Auth.Protos;
using Auth.Services.Abstract;
using Grpc.Core;

namespace Auth.Services.Grpc
{
    public class GrpcAuthService : AuthService.AuthServiceBase
    {
        private readonly IAuthValidationService _authValidationService;

        public GrpcAuthService(IAuthValidationService authValidationService)
        {
            _authValidationService = authValidationService;
        }

        public override async Task<TokenResponse> ValidateToken(TokenRequest request, ServerCallContext context)
        {
            var result = _authValidationService.ValidateTokenAsync(request.Token);
            
            return new TokenResponse
            {
                IsValid = result.IsValid,
                UserId = result.UserId ?? string.Empty,
                ErrorMessage = result.ErrorMessage ?? string.Empty
            };
        }
    }
} 