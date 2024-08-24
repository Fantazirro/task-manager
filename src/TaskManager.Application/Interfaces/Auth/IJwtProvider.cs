using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}