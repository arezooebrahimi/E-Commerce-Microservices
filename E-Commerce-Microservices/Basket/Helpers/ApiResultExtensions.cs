using Common.Exceptions;
using Common.WebFramework.Api;

namespace Basket.Helpers
{
    public static class ApiResultExtensions
    {
        public static ApiResult<T> ToApiSuccess<T>(this T data) where T : class
        {
            return new ApiResult<T>(true, ApiResultStatusCode.Success, data);
        }

        public static ApiResult<T> ToApiError<T>(this string errorMessage) where T : class
        {
            return new ApiResult<T>(false, ApiResultStatusCode.BadRequest, null, errorMessage);
        }
    }
}
