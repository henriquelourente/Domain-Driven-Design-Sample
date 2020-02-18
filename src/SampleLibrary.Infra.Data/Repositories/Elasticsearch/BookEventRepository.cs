using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<BookEvent>> GetByTextAsync(string text)
        {
            var searchResponse = await _context.GetClient()
                                .SearchAsync<BookEvent>(s =>
                                    s.Index(IndexName)
                                        .Query(q => q.MultiMatch(
                                            m => m.Fields(fs => fs.Field(f => f.Title)
                                                                  .Field(f => f.Author.Name)
                                                                  .Field(f => f.Publisher.Name))
                                            .Query(text))));

            return searchResponse.Documents;
        }
    }
}
