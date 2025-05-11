using Common.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Catalog.Product
{
    public class CreateProductRequest
    {
        public Guid? Id { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public required string Name { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public required string Slug { get; set; }

        [Display(Name = "توضیح کوتاه")]
        public string? ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "برند")]
        public Guid? BrandId { get; set; }

        [Display(Name = "نوع")]
        public short Type { get; set; }

        [Display(Name = "قیمت")]
        public long Price { get; set; }

        [Display(Name = "قیمت فروش")]
        public long? SalePrice { get; set; }

        [Display(Name = "تاریخ شروع فروش")]
        public DateTime? DateOnSaleFrom { get; set; }

        [Display(Name = "تاریخ پایان فروش")]
        public DateTime? DateOnSaleTo { get; set; }

        [Display(Name = "کامنت باز است؟")]
        public bool OpenedComments { get; set; }

        [Display(Name = "موجودی انبار")]
        public int StockQuantity { get; set; }

        [Display(Name = "مدیریت موجودی؟")]
        public bool ManageStock { get; set; }

        [Display(Name = "وضعیت موجودی")]
        public StockStatus StockStatus { get; set; }

        [Display(Name = "وضعیت")]
        public Status Status { get; set; }

        [Display(Name = "محصول پیشنهادی؟")]
        public bool IsSuggested { get; set; }

        [Display(Name = "نمایش در صفحه اصلی؟")]
        public bool DisplayOnHomePage { get; set; }

        [Display(Name = "ترتیب در صفحه اصلی")]
        public int OrderOnHomePage { get; set; }

        [Display(Name = "برچسب")]
        public string? Tag { get; set; }
    }
}
