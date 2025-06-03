namespace Common.Entities.Auth
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; }
    }
}
