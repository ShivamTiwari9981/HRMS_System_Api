using HRMS.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace HRMS.Application.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<string> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetAsync(string key, string value, TimeSpan expiry)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry
            };

            await _cache.SetStringAsync(key, value, options);
        }
    }
}
