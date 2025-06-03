using Auth.Data.Contexts;
using Auth.Services.Abstract;
using Common.Dtos.Auth;
using Common.Entities.Auth;
using Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Auth.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, AuthDbContext context, ITokenService tokenService)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                NameFamily = request.NameFamily,
                UserName = request.PhoneNumber,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new AppException(ApiResultStatusCode.BadRequest, errors);
            }

            await _userManager.AddToRoleAsync(user, "User");
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new AppException(ApiResultStatusCode.BadRequest, "Username or password is incorrect");

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            });

            await _context.SaveChangesAsync();

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var tokenEntity = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == request.RefreshToken);

            if (tokenEntity == null || tokenEntity.IsRevoked || tokenEntity.IsExpired)
                throw new AppException(ApiResultStatusCode.BadRequest, "Invalid or expired token");

            var user = await _userManager.FindByIdAsync(tokenEntity.UserId);
            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "User not found");

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = request.RefreshToken
            };
        }
    }
}
