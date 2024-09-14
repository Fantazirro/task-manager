using TaskManager.Application.Interfaces.Data;
using TaskManager.Infrastructure.Persistence.Repositories;

namespace TaskManager.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private ITaskRepository _taskRepository = null!;
        private IUserRepository _userRepository = null!;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITaskRepository TaskRepository =>
            _taskRepository ??= new TaskRepository(_dbContext);

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_dbContext);

        // TODO: использовать имя пользователя для CreatedBy и LastModifiedBy
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}