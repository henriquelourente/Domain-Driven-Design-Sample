using FluentAssertions;
using NUnit.Framework;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Domain.Entities.ValueObjects;
using SampleLibrary.Infra.Data.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace SampleLibrary.Integration.Tests.Controllers
{
    [TestFixture]
    public class BookControllerTests : ControllerBaseTests
    {
        private const string url = "api/book";

        private IBookRepository _bookRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _bookRepository = new BookRepository(GetContext());
        }

        [SetUp]
        public void SetUp()
        {
            StartDatabase();
            ResetDatabase();
        }

        [Theory]
        [TestCase("Pearson", HttpStatusCode.OK, 1)]
        [TestCase("", HttpStatusCode.BadRequest, 0)]
        public void Must_Add_Valid_Book(string bookName, HttpStatusCode httpStatusCode, int count)
        {
            var authorId = CreateAuthor();
            var publisherId = CreatePublisher();

            //Arrange
            var bookCommand = new CreateBookCommand { Title = bookName, AuthorId = authorId, PublisherId = publisherId, Publication = new PublicationCommand { Year = 2020, Edition = 3 } };

            //Act
            var httpResponseMessage = _httpClient.PostAsJsonAsync(url, bookCommand).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(httpStatusCode);
            (_bookRepository.GetAllAsync().Result).Count().Should().Be(count);
        }

        [Test]
        public void Must_Update_Valid_Book()
        {
            var book = CreateBook();

            //Arrange
            var bookCommand = CreateValidUpdateBookCommand(book);

            //Act
            var httpResponseMessage = _httpClient.PutAsJsonAsync(url, bookCommand).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var updatedBook = _bookRepository.GetById(bookCommand.Id);

            updatedBook.Title.Should().Be(bookCommand.Title);
        }


        [Test]
        public void Must_Not_Update_Valid_Book()
        {
            var book = CreateBook();

            //Arrange
            var bookCommand = CreateValidUpdateBookCommand(book);
            bookCommand.Title = string.Empty;

            //Act
            var httpResponseMessage = _httpClient.PutAsJsonAsync(url, bookCommand).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private Guid CreateAuthor()
        {
            var author = new Author("Eric Evans");
            var authorRepository = new AuthorRepository(GetContext());
            authorRepository.Add(author);
            authorRepository.SaveChanges();
            return author.Id;
        }

        private Guid CreatePublisher()
        {
            var publisher = new Publisher("Pearson");
            var publisherRepository = new PublisherRepository(GetContext());
            publisherRepository.Add(publisher);
            publisherRepository.SaveChanges();
            return publisher.Id;
        }

        private Book CreateBook()
        {
            var bookRepository = new BookRepository(GetContext());
            var authorId = CreateAuthor();
            var publisherId = CreatePublisher();
            var book = new Book("Domain Driven Design", authorId, publisherId, new Publication(3, 2020));

            bookRepository.Add(book);
            bookRepository.SaveChanges();

            return book;
        }

        private static UpdateBookCommand CreateValidUpdateBookCommand(Book book)
        {
            return new UpdateBookCommand
            {
                Id = book.Id,
                Title = "Clean Code",
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                Publication = new PublicationCommand { Year = 2020, Edition = 3 }
            };
        }

    }
}
