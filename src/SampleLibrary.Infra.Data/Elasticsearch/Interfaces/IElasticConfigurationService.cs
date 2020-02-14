namespace SampleLibrary.Infra.Data.Elasticsearch.Interfaces
{
    public interface IElasticConfigurationService
    {
        ElasticConfiguration Get();
    }

    public class ElasticConfiguration
    {
        public string[] Addresses { get; set; }
    }
}
