using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Suse.CloudPlatform.API.Stores
{
    public sealed class RedisCounterStore : ICounterStore<RedisCounterStore>
    {
        private readonly IRedisCacheClient _cacheClient;

        public RedisCounterStore(IRedisCacheClient cacheClient)
        {
            _cacheClient = cacheClient;
        }

        /// <inheritdoc />
        public async Task<long> IncrementAsync(string key, CancellationToken cancellation = default)
        {
            return await _cacheClient.Db0.Database.StringIncrementAsync(key);
        }
    }
}