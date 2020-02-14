using System;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using SampleLibrary.Infra.Data.Elasticsearch.Interfaces;

namespace SampleLibrary.Infra.Data.Elasticsearch
{
    public class ElasticContextProvider : IElasticContextProvider
    {
        private const string DEFAULT_CONNECTION = "http://localhost:9200/";
        private readonly IElasticConfigurationService _configuracaoService;
        private IElasticClient Client { get; set; }

        public ElasticContextProvider(IElasticConfigurationService configuracaoService)
        {
            _configuracaoService = configuracaoService;
        }

        public IElasticClient GetClient()
        {
            if (Client == null)
            {
                var hosts = _configuracaoService.Get().Addresses ?? new[] { DEFAULT_CONNECTION };

                if (hosts.Length > 1)
                {
                    var connectionPool = new SniffingConnectionPool(hosts.Select(a => new Uri(a)));
                    var settings = new ConnectionSettings(connectionPool);
                    Client = new ElasticClient(settings);
                }
                else
                {
                    var settings = new ConnectionSettings(new Uri(hosts[0]));
                    Client = new ElasticClient(settings);
                }
            }

            return Client;
        }
    }
}
