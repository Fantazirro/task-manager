namespace TaskManager.Application.Interfaces.Data
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IUserTaskRepository UserTaskRepository { get; }
        Task Commit();
    }
}