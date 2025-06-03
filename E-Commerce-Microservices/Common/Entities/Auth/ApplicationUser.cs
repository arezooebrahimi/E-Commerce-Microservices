using Microsoft.AspNetCore.Identity;

namespace Common.Entities.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public required string NameFamily { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new();
        public List<UserAddress> UserAddresses { get; set; } = new();
    }
}
