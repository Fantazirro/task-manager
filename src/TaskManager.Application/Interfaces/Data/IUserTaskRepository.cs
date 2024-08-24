using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Data
{
    public interface IUserTaskRepository : IGenericRepository<UserTask>
    {
        Task<IEnumerable<UserTask>> GetByUserId(Guid userId);
    }
}