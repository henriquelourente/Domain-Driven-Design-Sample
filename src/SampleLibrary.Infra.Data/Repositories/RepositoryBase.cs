using System;
using System.Threading.Tasks;
using SampleLibrary.Core.Entity;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Infra.Data.Context;

namespace SampleLibrary.Infra.Data.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : IEntity
    {
        protected readonly SampleLibraryContext _sampleLibraryContext;
        public IUnityOfWork UnitOfWork => _sampleLibraryContext;

        public RepositoryBase(SampleLibraryContext sampleLibraryContext)
        {
            _sampleLibraryContext = sampleLibraryContext;
        }

        public async Task<bool> Commit() => await UnitOfWork.Commit();

        public void Add(T entity) => _sampleLibraryContext.Add(entity);
        public void Update(T entity) => _sampleLibraryContext.Update(entity);

        public void Dispose()
        {
            _sampleLibraryContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
