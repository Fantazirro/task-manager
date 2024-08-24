using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Common;
using TaskManager.Infrastructure.Persistence.Repositories;

namespace TaskManager.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private IUserTaskRepository _userTaskRepository = null!;
        private IUserRepository _userRepository = null!;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserTaskRepository UserTaskRepository =>
            _userTaskRepository ??= new UserTaskRepository(_dbContext);

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_dbContext);

        // TODO: использовать имя пользователя для CreatedBy и LastModifiedBy
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}