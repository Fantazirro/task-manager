using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Models.Auth;

namespace TaskManager.Application.UseCases.Auth
{
    public class SendCode(ICacheService cacheService, IEmailSender emailSender) : IUseCase
    {
        public async Task Handle(string email)
        {
            var codeObject = new EmailConfirmCode()
            {
                Email = email,
                Code = new Random().Next(0, 10000)
            };

            var key = EmailConfirmCode.GetCacheKey(email);
            await cacheService.SetAsync(key, codeObject, TimeSpan.FromDays(1));

            await emailSender.SendMessage(
                email: email,
                subject: "Подтверждение адреса электронной почты",
                body: $"Код для подтверждения адреса электронной почты: {codeObject.Code:d4}");
        }
    }
}