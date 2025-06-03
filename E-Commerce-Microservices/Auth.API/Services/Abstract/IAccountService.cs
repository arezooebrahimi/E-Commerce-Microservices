using Common.Dtos.Auth;
using System.Security.Claims;

namespace Auth.Services.Abstract
{
    public interface IAccountService
    {
        Task<object> GetInfoAsync(ClaimsPrincipal user);
        Task ChangePasswordAsync(ClaimsPrincipal user, ChangePasswordRequest request);
        Task LogoutAsync(ClaimsPrincipal user, LogoutRequest request);
    }
}
