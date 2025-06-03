using Asp.Versioning;
using Auth.Services.Abstract;
using Common.Dtos.Auth;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers.v1
{
    [ApiVersion(1)]
    [Route("[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<string>> Register([FromBody] RegisterRequest request)
        {
            await _authService.RegisterAsync(request);
            return Ok("User created");
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<LoginResponse>> Refresh([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request);
            return Ok(result);
        }
    }
}
