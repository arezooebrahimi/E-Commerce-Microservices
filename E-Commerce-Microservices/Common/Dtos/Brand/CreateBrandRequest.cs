using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Catalog.Brand
{
    public class CreateBrandRequest
    {
        public Guid? Id { get; set; }

        [Display(Name = "نام برند")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
        public required string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }


        [Display(Name = "فعال است؟")]
        public bool IsActive { get; set; }
    }
}
