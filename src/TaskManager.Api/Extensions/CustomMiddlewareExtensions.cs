using TaskManager.Api.Middleware;

namespace TaskManager.Api.Extensions
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler();
        }
    }
}