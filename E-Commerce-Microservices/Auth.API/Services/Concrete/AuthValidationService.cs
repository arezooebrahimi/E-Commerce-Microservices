using Auth.Services.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Services.Concrete
{
    public class AuthValidationService : IAuthValidationService
    {
        private readonly IConfiguration _configuration;

        public AuthValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthTokenValidationResult ValidateTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new AuthTokenValidationResult 
                { 
                    IsValid = false, 
                    ErrorMessage = "Token is empty" 
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]!);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                return new AuthTokenValidationResult
                {
                    IsValid = true,
                    UserId = userId
                };
            }
            catch (SecurityTokenExpiredException)
            {
                return new AuthTokenValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Token has expired"
                };
            }
            catch (Exception)
            {
                return new AuthTokenValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Token is invalid"
                };
            }
        }
    }
} 