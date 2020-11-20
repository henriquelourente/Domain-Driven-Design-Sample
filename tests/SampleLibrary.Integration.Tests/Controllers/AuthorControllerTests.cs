using FluentAssertions;
using SampleLibrary.Domain.Commands.Author;
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
    public class AuthorControllerTests : ControllerBaseTests
    {
        private const string url = "api/author";

        private readonly IAuthorRepository _authorRepository;

        public AuthorControllerTests()
        {
            _authorRepository = new AuthorRepository(GetContext());
        }

        [Theory]
        [InlineData("Robert C. Martin", HttpStatusCode.OK, 1)]
        [InlineData("", HttpStatusCode.BadRequest, 0)]
        public void Must_Add_Valid_Author(string authorName, HttpStatusCode httpStatusCode, int count)
        {
            //Arrange
            var author = new CreateAuthorCommand { Name = authorName };

            //Act
            var httpResponseMessage = _httpClient.PostAsJsonAsync(url, author).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(httpStatusCode);
            (_authorRepository.GetAllAsync().Result).Count().Should().Be(count);
        }

        [Fact]
        public void Must_Update_Valid_Author()
        {
            //Arrange
            const string authorName = "Eric Evans";
            var addedAuthor = new Author("Martin Fowler");
            base.SeedData(addedAuthor);
            var author = new UpdateAuthorCommand { Id = addedAuthor.Id, Name = authorName };

            //Act
            var httpResponseMessage = _httpClient.PutAsJsonAsync(url, author).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var actual = (_authorRepository.GetAllAsync().Result).Single(a => a.Id == author.Id);
            actual.Name.Should().Be(authorName);
        }

        [Fact]
        public void Must_Not_Update_Invalid_Author()
        {
            //Arrange
            var author = new UpdateAuthorCommand { Id = Guid.Empty, Name = "Eric Evans" };

            //Act
            var httpResponseMessage = _httpClient.PutAsJsonAsync(url, author).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
