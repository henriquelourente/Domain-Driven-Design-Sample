using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Core.Messages;
using SampleLibrary.Infra.Data.Elasticsearch.Interfaces;

namespace SampleLibrary.Infra.Data.Repositories.Elasticsearch
{
    public abstract class ElasticSearchRepositoryBase<TDocument, TPrimaryKey>
         : IElasticsearchRepository<TDocument, TPrimaryKey>
         where TDocument : class, IMessage<TPrimaryKey>
         where TPrimaryKey : struct
    {
        protected readonly IElasticContextProvider _context;
        protected readonly string IndexName;

        protected ElasticSearchRepositoryBase(IElasticContextProvider context, string indexName)
        {
            _context = context;
            IndexName = indexName;
        }

        public TDocument Add(TDocument entity) => Index(entity);

        public async Task<TDocument> AddAsync(TDocument entity) => await IndexAsync(entity);

        public TDocument AddOrUpdate(TDocument entity) => Index(entity);

        public async Task<TDocument> AddOrUpdateAsync(TDocument entity) => await IndexAsync(entity);

        public Task<IEnumerable<TDocument>> GetAllAsync() => Get();

        public TDocument GetById(TPrimaryKey id)
        {
            var query = _context
                .GetClient()
                .Search(GetUsingId(id));
            return query.Documents.FirstOrDefault();
        }

        public async Task<TDocument> GetByIdAsync(TPrimaryKey id)
        {
            var query = await _context
                .GetClient()
                .SearchAsync(GetUsingId(id));
            return query.Documents.FirstOrDefault();
        }

        public void Remove(TPrimaryKey id) => Delete(id);

        public void Remove(TDocument entity) => Delete(entity.Id);

        public async Task<TDocument> RemoveAsync(TPrimaryKey id)
        {
            var entity = await GetByIdAsync(id);
            var response = await DeleteAsync(id);
            return entity;
        }

        public async Task<TDocument> RemoveAsync(TDocument entity)
        {
            var response = await DeleteAsync(entity.Id);
            return entity;
        }

        private DeleteByQueryResponse Delete(TPrimaryKey id)
        {
            return _context
                .GetClient()
                .DeleteByQuery<TDocument>(q => q
                    .Index(IndexName)
                    .Query(p => p.Match(f => f.Field("_id").Query(id.ToString()))));
        }

        private async Task<DeleteByQueryResponse> DeleteAsync(TPrimaryKey id)
        {
            return await _context
                .GetClient()
                .DeleteByQueryAsync<TDocument>(q => q
                    .Index(IndexName)
                    .Query(p => p.Match(f => f.Field("_id").Query(id.ToString()))));
        }

        private TDocument Index(TDocument entity)
        {
            var response = _context.GetClient()
                .Index(entity, idx => idx.Index(IndexName)
                .Id(new Id(entity.Id.ToString()))
                .Refresh(Refresh.WaitFor));

            return entity;
        }

        private async Task<TDocument> IndexAsync(TDocument entity)
        {
            var response = await _context.GetClient()
                .IndexAsync(entity, idx => idx.Index(IndexName)
                .Id(new Id(entity.Id.ToString()))
                .Refresh(Refresh.WaitFor));

            return entity;
        }

        private async Task<IEnumerable<TDocument>> Get()
        {
            var query = await _context
                .GetClient()
                .SearchAsync<TDocument>(idx => idx
                    .Index(IndexName)
                    .Size(1000)
                );
            return query.Documents;
        }

        private Func<SearchDescriptor<TDocument>, ISearchRequest> GetUsingId(TPrimaryKey id)
        {
            return idx => idx
                .Index(IndexName)
                .Query(
                    q => q
                    .Match(m => m
                        .Field(u => u.Id)
                        .Query(id.ToString())
                    )
                );
        }
    }
}
