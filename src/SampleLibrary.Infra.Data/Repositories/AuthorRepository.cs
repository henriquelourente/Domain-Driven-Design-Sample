using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Context;

namespace SampleLibrary.Infra.Data.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(SampleLibraryContext context)
            : base(context)
        {
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _sampleLibraryContext.Author.AnyAsync(a => a.Name.Equals(name));
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _sampleLibraryContext.Author.AsNoTracking().ToListAsync();
        }

    }
}