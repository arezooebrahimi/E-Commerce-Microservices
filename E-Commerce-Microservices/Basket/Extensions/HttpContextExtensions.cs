using Microsoft.AspNetCore.Http;

namespace Basket.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetUserId(this HttpContext context)
        {
            return context.Items["UserId"] as string;
        }

        public static bool IsAuthenticated(this HttpContext context)
        {
            return context.Items.ContainsKey("UserId");
        }
    }
} 