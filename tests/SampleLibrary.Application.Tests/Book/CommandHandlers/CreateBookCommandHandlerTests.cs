using System;
using Moq;
using SampleLibrary.Application.Book;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Book.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Application.Tests.Book.CommandHandlers
{
    public class CreateBookCommandHandlerTests
    {
        private readonly CreateBookCommandHandler _createBookCommandHandler;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly PublicationCommand _publication;
        private readonly Mock<IEventPublisher> _eventPublisher;

        public CreateBookCommandHandlerTests()
        {
            _publication = new PublicationCommand {Edition = 1, Year = DateTime.Now.Year};
            _bookRepository = new Mock<IBookRepository>();
            var publicationValidator = new PublicationCommandValidator();

            var createBookCommandValidator =
                new CreateBookCommandValidator(_bookRepository.Object, publicationValidator);

            _eventPublisher = new Mock<IEventPublisher>();

            _createBookCommandHandler =
                new CreateBookCommandHandler(createBookCommandValidator, _bookRepository.Object, _eventPublisher.Object);
        }

        [Fact]
        public void Should_Create_When_Command_Is_Valid()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                Title = "Clean Code", Publication = _publication, AuthorId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            //Act
            _createBookCommandHandler.Handle(createBookCommand);

            //Assert
            _bookRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Book>()), Times.Once);
            _eventPublisher.Verify(p => p.Publish(It.IsAny<IMessage>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Create_When_Command_Is_Invalid()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                Title = "", Publication = _publication, AuthorId = Guid.NewGuid(), PublisherId = Guid.NewGuid()
            };

            //Act
            _createBookCommandHandler.Handle(createBookCommand);

            //Assert
            _bookRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Book>()), Times.Never);
            _eventPublisher.Verify(p => p.Publish(It.IsAny<IMessage>()), Times.Never);
        }
    }
}