using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Context;

namespace SampleLibrary.Infra.Data.Repositories
{
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherRepository(SampleLibraryContext context)
            : base(context)
        {
        }

        public async Task<bool> Exists(string name)
        {
            return await _sampleLibraryContext.Publisher.AnyAsync(a => a.Name.Equals(name));
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await _sampleLibraryContext.Publisher.AsNoTracking().ToListAsync();
        }
    }
}