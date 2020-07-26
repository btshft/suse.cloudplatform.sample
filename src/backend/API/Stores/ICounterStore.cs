using System.Threading;
using System.Threading.Tasks;

namespace Suse.CloudPlatform.API.Stores
{
    public interface ICounterStore<TType>
    {
        public Task<long> IncrementAsync(string key, CancellationToken cancellation = default);
    }
}