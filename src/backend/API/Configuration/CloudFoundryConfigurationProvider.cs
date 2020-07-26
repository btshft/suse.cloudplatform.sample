using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Suse.CloudPlatform.API.Configuration
{
    internal class CloudFoundryConfigurationProvider : ConfigurationProvider
    {
        private readonly CloudFoundryConfigurationSource _source;

        public CloudFoundryConfigurationProvider(CloudFoundryConfigurationSource source)
        {
            _source = source;
        }

        /// <inheritdoc />
        public override void Load()
        {
            var servicesJson = Environment.GetEnvironmentVariable("VCAP_SERVICES");

            if (!string.IsNullOrWhiteSpace(servicesJson))
            {
                var servicesConfiguration = new ConfigurationBuilder()
                    .AddJsonStream(CreateMemoryStream(servicesJson))
                    .Build();

                if (servicesConfiguration != null)
                {
                    LoadChildren("vcap:services", servicesConfiguration.GetChildren());
                }
            }

            if (string.IsNullOrEmpty(servicesJson) && _source.FallbackFilePath != null)
            {
                var fallbackConfiguration = new ConfigurationBuilder()
                    .AddJsonFile(_source.FallbackFilePath, optional: true)
                    .Build();

                LoadChildren(prefix:null, fallbackConfiguration.GetChildren());
            }
        }

        internal static MemoryStream CreateMemoryStream(string json)
        {
            var memStream = new MemoryStream();
            var textWriter = new StreamWriter(memStream);
            textWriter.Write(json);
            textWriter.Flush();
            memStream.Seek(0, SeekOrigin.Begin);
            return memStream;
        }

        private void LoadChildren(string prefix, IEnumerable<IConfigurationSection> sections)
        {
            foreach (var section in sections)
            {
                LoadSection(prefix, section);
                LoadChildren(prefix, section.GetChildren());
            }
        }

        private void LoadSection(string prefix, IConfigurationSection section)
        {
            if (section == null)
                return;

            if (string.IsNullOrEmpty(section.Value))
                return;

            var key = string.IsNullOrWhiteSpace(prefix)
                ? section.Path
                : $"{prefix}{ConfigurationPath.KeyDelimiter}{section.Path}";

            Data[key] = section.Value;
        }
    }
}
