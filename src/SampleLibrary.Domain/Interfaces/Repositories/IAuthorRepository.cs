using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Entities;

namespace SampleLibrary.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<bool> ExistsAsync(string name);
        Task<IEnumerable<Author>> GetAllAsync();
    }
}