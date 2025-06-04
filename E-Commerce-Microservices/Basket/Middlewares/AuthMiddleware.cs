using Basket.Services.Grpc;

namespace Basket.API.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GrpcAuthClient _authService;

        public AuthMiddleware(RequestDelegate next, GrpcAuthClient authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var (isValid, userId, errorMessage) = await _authService.ValidateTokenAsync(token);

                if (isValid && !string.IsNullOrEmpty(userId))
                {
                    context.Items["UserId"] = userId;
                }
                else
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new { error = errorMessage ?? "Unauthorized" });
                    return;
                }
            }

            await _next(context);
        }
    }
} 