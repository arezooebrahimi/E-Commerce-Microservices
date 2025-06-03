using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Admin.FeatureOption
{
    public class CreateFeatureOptionRequest
    {
        public Guid? Id { get; set; }

        [Display(Name = "ویژگی")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public Guid FeatureId { get; set; }

        [Display(Name = "نام گزینه")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
        public required string Name { get; set; }

        [Display(Name = "نامک")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "{0} فقط می‌تواند شامل حروف کوچک، اعداد و خط تیره باشد.")]
        public string? Slug { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "ترتیب")]
        public int Order { get; set; }
    }

}
