using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Application.Interfaces.Services;

namespace TaskManager.Application.UseCases.Auth
{
    public class ConfirmResetPassword(IUnitOfWork unitOfWork, ICacheService cacheService, IPasswordHasher passwordHasher) : IUseCase
    {
        public record Request(Guid token, string password);

        public async Task<bool> Handle(Request request)
        {
            var key = $"resetPassword_{request.token}";

            var userId = await cacheService.GetAsync<Guid?>(key);
            if (userId is null) return false;

            await cacheService.RemoveAsync(key);

            var user = await unitOfWork.UserRepository.GetById(userId.Value);
            user!.PasswordHash = passwordHasher.Hash(request.password);
            unitOfWork.UserRepository.Update(user);

            await unitOfWork.Commit();

            return true;
        }
    }
}