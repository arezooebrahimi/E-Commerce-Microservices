using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Catalog.Feature
{
    public class CreateFeatureRequest
    {
        public Guid? Id { get; set; }

        [Display(Name = "نام ویژگی")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
        public required string Name { get; set; }

        [Display(Name = "نامک")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "{0} فقط می‌تواند شامل حروف کوچک، اعداد و خط تیره باشد.")]
        public required string Slug { get; set; }

        [Display(Name = "نوع")]
        public string? Type { get; set; }
    }
}
