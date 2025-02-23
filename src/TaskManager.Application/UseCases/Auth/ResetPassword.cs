using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Application.Interfaces.Services;

namespace TaskManager.Application.UseCases.Auth
{
    public class ResetPassword(IEmailSender emailSender, IUnitOfWork unitOfWork, ICacheService cacheService) : IUseCase
    {
        private string resetPasswordLink = "http://localhost:4200/reset-password?token=";

        public async Task<bool> Handle(string email)
        {         
            var user = await unitOfWork.UserRepository.GetByEmail(email);
            if (user is null) return false;

            var tokenId = Guid.NewGuid();
            await cacheService.SetAsync($"resetPassword_{tokenId}", user.Id, TimeSpan.FromDays(1));
            var link = resetPasswordLink + tokenId;

            await emailSender.SendMessage(
                email: email,
                subject: "Сброс пароля",
                body: $"Для сброса пароля <a href='{link}'>нажмите сюда</a>",
                isHtml: true);

            return true;
        }
    }
}