using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Entities;

namespace SampleLibrary.Domain.Interfaces.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<bool> ExistsAsync(string name);
        Task<IEnumerable<Publisher>> GetAllAsync();
    }
}