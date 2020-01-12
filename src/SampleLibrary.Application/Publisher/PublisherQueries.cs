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

        public Task<IEnumerable<Domain.Entities.Publisher>> GetAll()
        {
            return _publisherRepository.GetAll();
        }
    }

    public interface IPublisherQueries
    {
        Task<IEnumerable<Domain.Entities.Publisher>> GetAll();
    }
}