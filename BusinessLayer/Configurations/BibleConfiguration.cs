using BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Configurations
{
    public class BibleConfiguration : IBibleConfiguration
    {
        public string BaseAddress { get; set; }
        public string ReadingEndpoint { get; set; }
        public string ReadingTitleEndpoint { get; set; }
        public string SaintEndpoint { get; set; }

        public BibleConfiguration(IConfiguration configuration)
        {
            Bind(configuration);
        }
        public BibleConfiguration Bind(IConfiguration configuration)
        {
            configuration.GetSection("BibleConfiguration").Bind(this);
            return this;
        }
    }
}
