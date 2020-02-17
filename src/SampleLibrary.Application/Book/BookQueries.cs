using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Domain.Events;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Book
{
    public class BookQueries : IBookQueries
    {
        private readonly IBookEventRepository _bookEventRepository;

        public BookQueries(IBookEventRepository bookEventRepository)
        {
            _bookEventRepository = bookEventRepository;
        }

        public async Task<IEnumerable<BookEvent>> GetAllAsync()  => await _bookEventRepository.GetAllAsync();
    }

    public interface IBookQueries
    {
        Task<IEnumerable<BookEvent>> GetAllAsync();
    }
}