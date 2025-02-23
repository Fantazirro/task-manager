using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Data
{
    public interface ITaskRepository : IGenericRepository<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetByUserId(Guid userId);
        Task<IEnumerable<TaskEntity>> GetHistoryByUserId(Guid userId);
    }
}