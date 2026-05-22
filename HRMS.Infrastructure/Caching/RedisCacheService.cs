using HRMS.Application.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _database;
        public RedisCacheService(
            IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task SetAsync(string key,string value,TimeSpan expiry)
        {
            await _database.StringSetAsync(
                key,
                value,
                expiry);
        }

        public async Task<string> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
