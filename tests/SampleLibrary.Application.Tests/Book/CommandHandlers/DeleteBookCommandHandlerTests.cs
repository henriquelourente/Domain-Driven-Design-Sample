using System;
using System.Collections.Generic;
using System.Text;
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
    public class DeleteBookCommandHandlerTests
    {
        private readonly ICommandHandler<DeleteBookCommand> _deleteBookCommandHandler;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IEventPublisher<DeleteBookEvent>> _eventPublisher;
        private readonly Guid _bookId = Guid.NewGuid();

        public DeleteBookCommandHandlerTests()
        {
            var book = new Domain.Entities.Book("Clean Code", Guid.NewGuid(), Guid.NewGuid(), null);

            _bookRepository = new Mock<IBookRepository>();
            _bookRepository.Setup(r => r.GetById(_bookId)).Returns(book);

            var createBookCommandValidator = new DeleteBookCommandValidator(_bookRepository.Object);

            _eventPublisher = new Mock<IEventPublisher<DeleteBookEvent>>();

            _deleteBookCommandHandler =
                new DeleteBookCommandHandler(createBookCommandValidator, _bookRepository.Object, _eventPublisher.Object);
        }

        [Fact]
        public void Should_Remove_When_Command_Is_Valid()
        {
            //Arrange
            var deleteBookCommand = new DeleteBookCommand { Id =_bookId };

            //Act
            _deleteBookCommandHandler.Handle(deleteBookCommand);

            //Assert
            _bookRepository.Verify(r => r.Delete(_bookId), Times.Once);
            _eventPublisher.Verify(p => p.Publish(It.IsAny<DeleteBookEvent>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Remove_When_Command_Is_Invalid()
        {
            //Arrange
            var deleteBookCommand = new DeleteBookCommand { Id = Guid.NewGuid() };

            //Act
            _deleteBookCommandHandler.Handle(deleteBookCommand);

            //Assert
            _bookRepository.Verify(r => r.Delete(It.IsAny<Guid>()), Times.Never);
            _eventPublisher.Verify(p => p.Publish(It.IsAny<DeleteBookEvent>()), Times.Never);
        }
    }
}
