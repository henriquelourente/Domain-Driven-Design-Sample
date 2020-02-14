using System;
using SampleLibrary.Domain.Events;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Elasticsearch.Interfaces;

namespace SampleLibrary.Infra.Data.Repositories.Elasticsearch
{
    public class BookEventRepository : ElasticSearchRepositoryBase<BookEvent, Guid>, IBookEventRepository
    {
        public BookEventRepository(IElasticContextProvider context)
            : base(context, "book")
        {
        }
    }
}
