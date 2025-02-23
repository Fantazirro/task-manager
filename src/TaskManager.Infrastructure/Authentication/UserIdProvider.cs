using Microsoft.AspNetCore.Http;

namespace TaskManager.Infrastructure.Authentication
{
    public class UserIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        public Guid? GetUserId()
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null) return null;

            var userId = httpContext.User.FindFirst("UserId");
            return userId is null ? null : Guid.Parse(userId.Value);
        }
    }
}