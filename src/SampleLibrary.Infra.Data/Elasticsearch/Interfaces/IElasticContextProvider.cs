using Nest;

namespace SampleLibrary.Infra.Data.Elasticsearch.Interfaces
{
    public interface IElasticContextProvider
    {
        IElasticClient GetClient();
    }
}
