using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Utf8Json;
using Suse.CloudPlatform.API.Connectors;
using Suse.CloudPlatform.API.Connectors.Options;
using Suse.CloudPlatform.API.Stores;

namespace Suse.CloudPlatform.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRedisConnectorOptions(Configuration);

            services.AddStackExchangeRedisExtensions<Utf8JsonSerializer>(sp =>
            {
                var monitor = sp.GetRequiredService<IOptionsMonitor<RedisConnectorOptions>>();
                var options = monitor.Get("default-redis");

                return new RedisConfiguration
                {
                    Password = options.Password,
                    Hosts = new[]
                    {
                        new RedisHost
                        {
                            Port = options.Port,
                            Host = options.Host
                        }
                    }
                };
            });

            services.AddSingleton<ICounterStore<InMemoryCounterStore>, InMemoryCounterStore>();
            services.AddSingleton<ICounterStore<RedisCounterStore>, RedisCounterStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(o => o
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
