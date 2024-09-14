namespace TaskManager.Application.Interfaces.Data
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ITaskRepository TaskRepository { get; }
        Task Commit();
    }
}