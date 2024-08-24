using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence.Repositories
{
    public class UserTaskRepository : GenericRepository<UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<UserTask>> GetByUserId(Guid userId)
        {
            return await _dbContext.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }
    }
}