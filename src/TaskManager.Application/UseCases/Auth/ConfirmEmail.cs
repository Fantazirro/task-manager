using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Models.Auth;

namespace TaskManager.Application.UseCases.Auth
{
    public class ConfirmEmail(ICacheService cacheService) : IUseCase
    {
        public record Request(string email, int code);

        public async Task<bool> Handle(Request request)
        {
            var key = EmailConfirmCode.GetCacheKey(request.email);
            var codeObject = await cacheService.GetAsync<EmailConfirmCode>(key);

            if (codeObject is null || codeObject.Code != request.code) return false;

            await cacheService.RemoveAsync(key);

            return true;
        }
    }
}