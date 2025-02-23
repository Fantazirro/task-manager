using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<TaskEntity>> GetByUserId(Guid userId)
        {
            return await _dbContext.Tasks
                .Where(t => t.UserId == userId && t.Status != TaskEntityStatus.InHistory)
                .OrderByDescending(t => t.CreatedOnUtc)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetHistoryByUserId(Guid userId)
        {
            return await _dbContext.Tasks
                .Where(t => t.UserId == userId && t.Status == TaskEntityStatus.InHistory)
                .OrderByDescending(t => t.CreatedOnUtc)
                .ToListAsync();
        }
    }
}