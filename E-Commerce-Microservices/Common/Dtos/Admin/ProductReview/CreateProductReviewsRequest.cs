using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Admin.ProductReview
{
    public class CreateProductReviewsRequest
    {
        public Guid? Id { get; set; }

        [Display(Name = "عنوان نظر")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public required string Title { get; set; }

        [Display(Name = "توضیحات نظر")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public required string ReviewText { get; set; }
        public int Rating { get; set; }
    }
}
