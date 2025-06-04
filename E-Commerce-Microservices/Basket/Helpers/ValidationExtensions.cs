using Common.Exceptions;
using Common.WebFramework.Api;
using FluentValidation.Results;

namespace Basket.Helpers
{
    public static class ValidationExtensions
    {
        public static ApiResult ToValidationErrorResult(this ValidationResult result)
        {
            var errors = result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
            var message = string.Join(" | ", errors);
            return new ApiResult(false, ApiResultStatusCode.BadRequest, message);
        }
    }
}
