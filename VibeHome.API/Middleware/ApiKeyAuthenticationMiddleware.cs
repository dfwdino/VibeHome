using VibeHome.Application.Interfaces;

namespace VibeHome.API.Middleware
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER = "X-API-Key";

        public ApiKeyAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApiKeyService apiKeyService)
        {
            // Skip authentication for Swagger UI and health check endpoints
            if (context.Request.Path.StartsWithSegments("/swagger") ||
                context.Request.Path.StartsWithSegments("/health") ||
                context.Request.Path == "/")
            {
                await _next(context);
                return;
            }

            // Check if API key is present in header
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var apiKeyHeaderValues))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "API Key is missing",
                    message = $"Please provide a valid API key in the '{API_KEY_HEADER}' header"
                });
                return;
            }

            var apiKey = apiKeyHeaderValues.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "API Key is invalid",
                    message = "The provided API key is empty"
                });
                return;
            }

            // Validate API key
            var isValid = await apiKeyService.ValidateApiKeyAsync(apiKey);
            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Forbidden",
                    message = "The provided API key is invalid, expired, or inactive"
                });
                return;
            }

            // API key is valid, continue to next middleware
            await _next(context);
        }
    }

    public static class ApiKeyAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyAuthenticationMiddleware>();
        }
    }
}
