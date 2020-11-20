using FluentAssertions;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleLibrary.Integration.Tests.Controllers
{
    public class PublisherControllerTests : ControllerBaseTests
    {
        private const string url = "api/publisher";

        private readonly IPublisherRepository _publisherRepository;

        public PublisherControllerTests()
        {
            _publisherRepository = new PublisherRepository(GetContext());
        }

        [Theory]
        [InlineData("Pearson", HttpStatusCode.OK, 1)]
        [InlineData("", HttpStatusCode.BadRequest, 0)]
        public void Must_Add_Valid_Publisher(string publisherName, HttpStatusCode httpStatusCode, int count)
        {
            //Arrange
            var publisher = new CreatePublisherCommand { Name = publisherName };

            //Act
            var httpResponseMessage = _httpClient.PostAsJsonAsync(url, publisher).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(httpStatusCode);
            (_publisherRepository.GetAllAsync().Result).Count().Should().Be(count);
        }

        [Fact]
        public void Must_Update_Valid_Publisher()
        {
            //Arrange
            const string publisherName = "O'Reilly";
            var addedPublisher = new Publisher("Casa do Código");
            base.SeedData(addedPublisher);
            var publisher = new UpdatePublisherCommand { Id = addedPublisher.Id, Name = publisherName };

            //Act
            var httpResponseMessage = _httpClient.PutAsJsonAsync(url, publisher).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var actual = (_publisherRepository.GetAllAsync().Result).Single(a => a.Id == publisher.Id);
            actual.Name.Should().Be(publisherName);
        }

        [Fact]
        public void Must_Not_Update_Invalid_Publisher()
        {
            //Arrange
            var publisher = new UpdatePublisherCommand { Id = Guid.Empty, Name = "O'Reilly" };

            //Act
            var httpResponseMessage = _httpClient.PutAsJsonAsync(url, publisher).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
