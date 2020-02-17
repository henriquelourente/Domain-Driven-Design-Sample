using System;
using Moq;
using SampleLibrary.Application.Book;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Book.Validators;
using SampleLibrary.Domain.Events;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Application.Tests.Book.CommandHandlers
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly UpdateBookCommandHandler _updateBookCommandHandler;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly PublicationCommand _publication;
        private readonly Mock<IEventPublisher<BookEvent>> _eventPublisher;

        public UpdateBookCommandHandlerTests()
        {
            _publication = new PublicationCommand {Edition = 1, Year = DateTime.Now.Year};
            _bookRepository = new Mock<IBookRepository>();
            var publicationValidator = new PublicationCommandValidator();

            var updateBookCommandValidator =
                new UpdateBookCommandValidator(_bookRepository.Object, publicationValidator);

            _eventPublisher = new Mock<IEventPublisher<BookEvent>>();

            _updateBookCommandHandler =
                new UpdateBookCommandHandler(updateBookCommandValidator, _bookRepository.Object, _eventPublisher.Object);
        }

        [Fact]
        public void Should_Update_When_Command_Is_Valid()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                Id = Guid.NewGuid(), Title = "Clean Code", Publication = _publication, AuthorId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            //Act
            _updateBookCommandHandler.Handle(updateBookCommand);

            //Assert
            _bookRepository.Verify(r => r.Update(It.IsAny<Domain.Entities.Book>()), Times.Once);
            _eventPublisher.Verify(p => p.Publish(It.IsAny<BookEvent>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Update_When_Command_Is_Invalid()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                Title = "Clean Code", Publication = _publication, AuthorId = Guid.NewGuid(), PublisherId = Guid.NewGuid()
            };

            //Act
            _updateBookCommandHandler.Handle(updateBookCommand);

            //Assert
            _bookRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Book>()), Times.Never);
            _eventPublisher.Verify(p => p.Publish(It.IsAny<BookEvent>()), Times.Never);
        }
    }
}