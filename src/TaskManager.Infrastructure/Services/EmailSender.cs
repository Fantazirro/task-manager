using FluentEmail.Core;
using TaskManager.Application.Interfaces.Auth;

namespace TaskManager.Infrastructure.Services
{
    public class EmailSender(IFluentEmail fluentEmail) : IEmailSender
    {
        public async Task SendMessage(string email, string subject, string body, bool isHtml = false)
        {
            await fluentEmail
                .To(email)
                .Subject(subject)
                .Body(body, isHtml)
                .SendAsync();
        }
    }
}