using System;
using System.Threading.Tasks;
using SampleLibrary.Core.Entity;

namespace SampleLibrary.Core.Interfaces
{
    public interface IRepository<in T> : IDisposable where T : IEntity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<bool> Commit();

        void Add(T entity);
        void Update(T entity);
    }

    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
