namespace TaskManager.Application.Interfaces.Services
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan timeSpan);
        Task RemoveAsync(string key);
    }
}