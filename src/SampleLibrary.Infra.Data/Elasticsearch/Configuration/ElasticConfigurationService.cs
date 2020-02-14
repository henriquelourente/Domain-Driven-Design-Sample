using Microsoft.Extensions.Configuration;
using SampleLibrary.Infra.Data.Elasticsearch.Interfaces;

namespace SampleLibrary.Infra.Data.Elasticsearch.Configuration
{
    public class ElasticConfigurationService : IElasticConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ElasticConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ElasticConfiguration Get() => _configuration.GetSection("Elastic").Get<ElasticConfiguration>();
    }
}
