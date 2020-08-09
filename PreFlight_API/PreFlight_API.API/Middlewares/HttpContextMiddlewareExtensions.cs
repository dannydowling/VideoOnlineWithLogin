using Microsoft.AspNetCore.Builder;

namespace PreFlight_API.API.Middleware
{
    public static class HttpContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpContextMiddleware>();
        }
    }
}




