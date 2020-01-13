using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Entities;

namespace SampleLibrary.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<bool> Exists(string title);
        Task<IEnumerable<Book>> GetAll();
    }
}