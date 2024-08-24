namespace TaskManager.Application.Interfaces.Data
{
    public interface IGenericRepository<T>
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}