

using FluentValidation;

namespace Basket.Models
{
    public class RemoveBasketItemRequest
    {
        public string? BasketId { get; set; }
        public string ProductId { get; set; } = default!;
        public int? Quantity { get; set; }
        public string? FeatureOptionId { get; set; }
    }


    public class RemoveBasketItemRequestValidator : AbstractValidator<RemoveBasketItemRequest>
    {
        public RemoveBasketItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .NotNull().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.FeatureOptionId)
                .MaximumLength(100).WithMessage("FeatureOptionId cannot be longer than 100 characters.");
        }
    }
}
