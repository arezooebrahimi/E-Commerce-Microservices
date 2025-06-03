using Auth.Data.Contexts;
using Auth.Services.Abstract;
using Common.Dtos.Auth;
using Common.Entities.Auth;
using Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Auth.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _dbContext;

        public AccountService(UserManager<ApplicationUser> userManager, AuthDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<object> GetInfoAsync(ClaimsPrincipal userClaims)
        {
            var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new AppException(ApiResultStatusCode.UnAuthorized, "کاربر یافت نشد");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new AppException(ApiResultStatusCode.UnAuthorized, "کاربر یافت نشد");

            return new
            {
                user.NameFamily,
                Role = userClaims.FindFirst(ClaimTypes.Role)?.Value
            };
        }

        public async Task ChangePasswordAsync(ClaimsPrincipal userClaims, ChangePasswordRequest request)
        {
            var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new AppException(ApiResultStatusCode.UnAuthorized, "User not found");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new AppException(ApiResultStatusCode.UnAuthorized, "User not found");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new AppException(ApiResultStatusCode.BadRequest, errors);
            }
        }

        public async Task LogoutAsync(ClaimsPrincipal userClaims, LogoutRequest request)
        {
            var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new AppException(ApiResultStatusCode.UnAuthorized, "User not found");

            var token = await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == request.RefreshToken && x.UserId == userId);

            if (token == null)
                throw new AppException(ApiResultStatusCode.NotFound, "Refresh token not found");

            if (token.IsRevoked)
                throw new AppException(ApiResultStatusCode.BadRequest, "Token is already revoked");

            token.IsRevoked = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
