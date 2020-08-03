using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suse.CloudPlatform.API.Connectors.Models;
using Suse.CloudPlatform.API.Connectors.Options;

namespace Suse.CloudPlatform.API.Connectors
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisConnectorOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceSection = configuration.GetSection("vcap:services");
            if (!serviceSection.Exists())
            {
                return services;
            }

            var definition = serviceSection.Get<ServicesDefinition>();

            foreach (var redis in definition.Redis)
            {
                var redisRef = redis;

                services.AddOptions<RedisConnectorOptions>(redis.Name)
                    .Configure(o =>
                    {
                        o.Host = redisRef.Credentials.Host;
                        o.Name = redisRef.Name;
                        o.Password = redisRef.Credentials.Password;
                        o.Port = redisRef.Credentials.Port;
                    });
            }

            return services;
        }
    }
}
