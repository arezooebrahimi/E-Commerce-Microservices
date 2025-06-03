using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Auth
{
    public record LoginRequest(
        [Required(ErrorMessage = "شماره موبایل الزامی است.")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست.")]
        string PhoneNumber,

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        string Password
    );
}
