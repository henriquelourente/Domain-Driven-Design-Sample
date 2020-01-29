using System;
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
        public IUnityOfWork UnitOfWork => _sampleLibraryContext;

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

        public async Task<bool> ExistsAsync(string title)
        {
            return await _sampleLibraryContext.Book.AnyAsync(b => b.Title.Equals(title));
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _sampleLibraryContext.Book.AsNoTracking().ToListAsync();
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await _sampleLibraryContext.Book
                .AsNoTracking()
                .Include(b => b.Publisher)
                .Include(b => b.Publication)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<bool> Commit()
        {
            return UnitOfWork.Commit();
        }
    }
}