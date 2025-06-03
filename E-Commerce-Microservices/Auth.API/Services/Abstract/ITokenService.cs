using Common.Entities.Auth;

namespace Auth.Services.Abstract
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser user);
        Task<string> GenerateRefreshTokenAsync();
    }
}
