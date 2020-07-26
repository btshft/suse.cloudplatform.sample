using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Suse.CloudPlatform.API.Models;
using Suse.CloudPlatform.API.Stores;

namespace Suse.CloudPlatform.API.Controllers
{
    [ApiController, Route("counters")]
    public class CountersController : ControllerBase
    {
        private readonly ICounterStore<InMemoryCounterStore> _inMemoryCounterStore;
        private readonly ICounterStore<RedisCounterStore> _redisCounterStore;

        public CountersController(ICounterStore<InMemoryCounterStore> inMemoryCounterStore, ICounterStore<RedisCounterStore> redisCounterStore)
        {
            _inMemoryCounterStore = inMemoryCounterStore;
            _redisCounterStore = redisCounterStore;
        }

        [HttpPost("local")]
        public async Task<IActionResult> IncrementLocal(CounterInput input, CancellationToken cancellation = default)
        {
            var value = await _inMemoryCounterStore.IncrementAsync(input.Key, cancellation);
            return Ok(new CounterValue
            {
                Key = input.Key,
                Value = value
            });
        }

        [HttpPost("global")]
        public async Task<IActionResult> IncrementGlobal(CounterInput input, CancellationToken cancellation = default)
        {
            var value = await _redisCounterStore.IncrementAsync(input.Key, cancellation);
            return Ok(new CounterValue
            {
                Key = input.Key,
                Value = value
            });
        }
    }
}
