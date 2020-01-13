using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Context;

namespace SampleLibrary.Infra.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly SampleLibraryContext _sampleLibraryContext;
        public IUnitOfWork UnitOfWork => _sampleLibraryContext;

        public BookRepository(SampleLibraryContext context)
        {
            _sampleLibraryContext = context;
        }

        public void Add(Book entity)
        {
            _sampleLibraryContext.Book.Add(entity);
        }

        public void Update(Book entity)
        {
            _sampleLibraryContext.Book.Update(entity);
        }

        public async Task<bool> Exists(string name)
        {
            return await _sampleLibraryContext.Book.AnyAsync(b => b.Title.Equals(name));
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _sampleLibraryContext.Book.AsNoTracking().ToListAsync();
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