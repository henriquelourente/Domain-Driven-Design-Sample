using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Core.Messages;

namespace SampleLibrary.Core.Interfaces
{
    public interface IElasticsearchRepository<TMessage, TPrimaryKey> :
     IPersistentRepository<TMessage, TPrimaryKey>,
     ISearchableRepository<TMessage, TPrimaryKey>
     where TMessage : class, IMessage<TPrimaryKey>
    {
    }

    public interface IPersistentRepository<TMessage, TPrimaryKey>
    where TMessage : class, IMessage<TPrimaryKey>
    {
        TMessage Add(TMessage entity);
        Task<TMessage> AddAsync(TMessage entity);

        TMessage AddOrUpdate(TMessage entity);
        Task<TMessage> AddOrUpdateAsync(TMessage entity);

        void Remove(TPrimaryKey id);
        Task<TMessage> RemoveAsync(TPrimaryKey id);

        void Remove(TMessage entity);
        Task<TMessage> RemoveAsync(TMessage entity);
    }

    public interface ISearchableRepository<TMessage, TPrimaryKey> where TMessage : class
    {
        TMessage GetById(TPrimaryKey id);
        Task<TMessage> GetByIdAsync(TPrimaryKey id);
        Task<IEnumerable<TMessage>> GetAllAsync();

    }
}
