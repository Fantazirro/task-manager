using Enyim.Caching;
using TaskManager.Application.Interfaces.Services;

namespace TaskManager.Infrastructure.Services
{
    public class CacheService(IMemcachedClient memcachedClient) : ICacheService
    {
        public async Task<T?> GetAsync<T>(string key)
        {
            return await memcachedClient.GetValueAsync<T>(key);
        }

        public async Task RemoveAsync(string key)
        {
            await memcachedClient.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan timeSpan)
        {
            await memcachedClient.SetAsync(key, value, timeSpan);
        }
    }
}