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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly SampleLibraryContext _sampleLibraryContext;
        public IUnityOfWork UnitOfWork => _sampleLibraryContext;

        public AuthorRepository(SampleLibraryContext context)
        {
            _sampleLibraryContext = context;
        }

        public void Add(Author entity)
        {
            _sampleLibraryContext.Author.Add(entity);
        }

        public void Update(Author entity)
        {
            _sampleLibraryContext.Author.Update(entity);
        }

        public async Task<bool> Exists(string name)
        {
            return await _sampleLibraryContext.Author.AnyAsync(a => a.Name.Equals(name));
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _sampleLibraryContext.Author.AsNoTracking().ToListAsync();
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