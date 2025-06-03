
using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Auth
{
    public record RefreshTokenRequest(
        [Required(ErrorMessage = "توکن الزامی است.")]
        [MinLength(20, ErrorMessage = "توکن نامعتبر است.")]
        string RefreshToken
    );
}
