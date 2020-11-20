using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Publisher
{
    public class PublisherQueries : IPublisherQueries
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherQueries(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Publisher>> GetAllAsync()
        {
            return await _publisherRepository.GetAllAsync();
        }
    }

    public interface IPublisherQueries
    {
        Task<IEnumerable<Domain.Entities.Publisher>> GetAllAsync();
    }
}