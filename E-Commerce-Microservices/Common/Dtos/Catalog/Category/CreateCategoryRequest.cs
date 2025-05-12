

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Catalog.Category
{
    public class CreateCategoryRequest
    {
        public Guid? Id { get; set; }
        public Guid? ParentId { get; set; }

        [Display(Name = "نام دسته")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
        public required string Name { get; set; }
        public string? Description { get; set; }


        [Display(Name = "نامک")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد.")]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "{0} فقط می‌تواند شامل حروف کوچک، اعداد و خط تیره باشد.")]
        public required string Slug { get; set; }
        public int Order { get; set; }
        public bool DisplayOnHomePage { get; set; }
        public int OrderOnHomePage { get; set; }

        public IFormFile? ImageOnHomePage { get; set; }
        public List<IFormFile>? Medias { get; set; }
    }
}
