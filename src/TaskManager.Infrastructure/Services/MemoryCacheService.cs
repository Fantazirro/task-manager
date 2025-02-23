using Microsoft.Extensions.Caching.Memory;
using TaskManager.Application.Interfaces.Services;

namespace TaskManager.Infrastructure.Services
{
    public class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
    {
        public Task<T> GetAsync<T>(string key)
        {
            return Task.Run(() => memoryCache.Get<T?>(key));
        }

        public Task RemoveAsync(string key)
        {
            return Task.Run(() => memoryCache.Remove(key));
        }

        public Task SetAsync<T>(string key, T value, TimeSpan timeSpan)
        {
            return Task.Run(() => memoryCache.Set<T>(key, value, timeSpan));
        }
    }
}