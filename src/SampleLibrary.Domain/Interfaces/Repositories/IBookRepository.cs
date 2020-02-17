using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Entities;

namespace SampleLibrary.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<bool> ExistsAsync(string title);
        Task<IEnumerable<Book>> GetAllAsync();
        Book GetById(Guid id);
        void Delete(Guid id);
    }
}