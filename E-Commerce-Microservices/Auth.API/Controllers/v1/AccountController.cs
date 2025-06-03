using Auth.Services.Abstract;
using Common.Dtos.Auth;
using Common.WebFramework.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers.v1
{
    [Authorize]
    [Route("[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("[action]")]
        public async Task<ApiResult<object>> GetInfo()
        {
            var result = await _accountService.GetInfoAsync(User);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<object>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            await _accountService.ChangePasswordAsync(User, request);
            return Ok("Password changed successfully");
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<object>> Logout([FromBody] LogoutRequest request)
        {
            await _accountService.LogoutAsync(User, request);
            return Ok("Logout successful");
        }
    }
}
