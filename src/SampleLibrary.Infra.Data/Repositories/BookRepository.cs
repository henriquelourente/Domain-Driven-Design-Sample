using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Context;

namespace SampleLibrary.Infra.Data.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(SampleLibraryContext context)
            : base (context)
        {
        }

        public async Task<bool> ExistsAsync(string title)
        {
            return await _sampleLibraryContext.Book.AnyAsync(b => b.Title.Equals(title));
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _sampleLibraryContext.Book.AsNoTracking().ToListAsync();
        }

        public Book GetById(Guid id)
        {
            return _sampleLibraryContext.Book
                .AsNoTracking()
                .Include(b => b.Publisher)
                .Include(b => b.Publication)
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);
        }
    }
}