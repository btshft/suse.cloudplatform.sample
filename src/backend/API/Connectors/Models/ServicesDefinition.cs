using System;

namespace Suse.CloudPlatform.API.Connectors.Models
{
    public class ServicesDefinition
    {
        public RedisService[] Redis { get; set; } = Array.Empty<RedisService>();

        public class RedisService
        {
            public string Name { get; set; }

            public RedisCredentials Credentials { get; set; }

            public class RedisCredentials
            {
                public string Host { get; set; }

                public string Password { get; set; }

                public int Port { get; set; }
            }
        }
    }
}