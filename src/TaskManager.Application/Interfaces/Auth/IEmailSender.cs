namespace TaskManager.Application.Interfaces.Auth
{
    public interface IEmailSender
    {
        Task SendMessage(string email, string subject, string body, bool isHtml = false);
    }
}