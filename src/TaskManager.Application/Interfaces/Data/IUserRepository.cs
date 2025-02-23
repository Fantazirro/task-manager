using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Data
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmail(string email);
    }
}