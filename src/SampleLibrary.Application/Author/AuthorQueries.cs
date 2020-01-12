using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Author
{
    public class AuthorQueries : IAuthorQueries
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorQueries(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Task<IEnumerable<Domain.Entities.Author>> GetAll()
        {
            return _authorRepository.GetAll();
        }
    }

    public interface IAuthorQueries
    {
        Task<IEnumerable<Domain.Entities.Author>> GetAll();
    }
}