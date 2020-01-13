using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities;

namespace SampleLibrary.Domain.Interfaces.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<bool> Exists(string name);
        Task<IEnumerable<Publisher>> GetAll();
    }
}