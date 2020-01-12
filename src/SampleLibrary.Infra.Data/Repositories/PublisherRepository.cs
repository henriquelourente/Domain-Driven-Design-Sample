using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Context;

namespace SampleLibrary.Infra.Data.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly SampleLibraryContext _sampleLibraryContext;
        public IUnitOfWork UnitOfWork => _sampleLibraryContext;

        public PublisherRepository(SampleLibraryContext context)
        {
            _sampleLibraryContext = context;
        }

        public void Add(Publisher entity)
        {
            _sampleLibraryContext.Publisher.Add(entity);
        }

        public void Update(Publisher entity)
        {
            _sampleLibraryContext.Publisher.Update(entity);
        }

        public async Task<bool> Exists(string name)
        {
            return await _sampleLibraryContext.Publisher.AnyAsync(a => a.Name.Equals(name));
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await _sampleLibraryContext.Publisher.AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            _sampleLibraryContext?.Dispose();
        }

        public Task<bool> Commit()
        {
            return UnitOfWork.Commit();
        }
    }
}