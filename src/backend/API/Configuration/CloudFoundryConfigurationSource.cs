using Microsoft.Extensions.Configuration;

namespace Suse.CloudPlatform.API.Configuration
{
    internal class CloudFoundryConfigurationSource : IConfigurationSource
    {
        public string FallbackFilePath { get; set; }

        /// <inheritdoc />
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CloudFoundryConfigurationProvider(this);
        }
    }
}