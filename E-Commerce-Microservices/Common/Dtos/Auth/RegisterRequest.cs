using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Auth
{
    public record RegisterRequest(
        [Required(ErrorMessage = "نام و نام خانوادگی الزامی است.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "نام و نام خانوادگی باید بین 2 تا 100 کاراکتر باشد.")]
        string NameFamily,

        [Required(ErrorMessage = "شماره موبایل الزامی است.")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست.")]
        string PhoneNumber,

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد.")]
        string Password
    );
}
