using Microsoft.Extensions.Configuration;

namespace Suse.CloudPlatform.API.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddCloudFoundry(this IConfigurationBuilder builder,
            string fallbackFilePath = null)
        {
            return builder.Add(new CloudFoundryConfigurationSource { FallbackFilePath = fallbackFilePath });
        }
    }
}