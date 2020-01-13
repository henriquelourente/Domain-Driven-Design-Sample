using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Book
{
    public class BookQueries : IBookQueries
    {
        private readonly IBookRepository _bookRepository;

        public BookQueries(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<IEnumerable<Domain.Entities.Book>> GetAll()
        {
            return _bookRepository.GetAll();
        }
    }

    public interface IBookQueries
    {
        Task<IEnumerable<Domain.Entities.Book>> GetAll();
    }
}