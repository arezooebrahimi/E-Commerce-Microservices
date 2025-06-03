using System.Threading.Tasks;

namespace Auth.Services.Abstract
{
    public interface IAuthValidationService
    {
        AuthTokenValidationResult ValidateTokenAsync(string token);
    }

    public class AuthTokenValidationResult
    {
        public bool IsValid { get; set; }
        public string? UserId { get; set; }
        public string? ErrorMessage { get; set; }
    }
} 