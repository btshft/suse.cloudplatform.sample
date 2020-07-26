using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Suse.CloudPlatform.API.Stores
{
    public sealed class InMemoryCounterStore : ICounterStore<InMemoryCounterStore>
    {
        private readonly ConcurrentDictionary<string, long> _counters;

        public InMemoryCounterStore()
        {
            _counters = new ConcurrentDictionary<string, long>();
        }

        /// <inheritdoc />
        public Task<long> IncrementAsync(string key, CancellationToken cancellation = default)
        {
            var value = _counters.AddOrUpdate(key, k => 1, (s, l) => l + 1);
            return Task.FromResult(value);
        }
    }
}