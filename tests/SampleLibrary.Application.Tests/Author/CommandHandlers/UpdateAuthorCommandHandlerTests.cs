using System;
using FluentValidation;
using Moq;
using SampleLibrary.Application.Author;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Commands.Author.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Application.Tests.Author.CommandHandlers
{
    public class UpdateAuthorCommandHandlerTests
    {
        private readonly UpdateAuthorCommandHandler _updateAuthorCommandHandler;
        private readonly Mock<IAuthorRepository> _authorRepository;

        public UpdateAuthorCommandHandlerTests()
        {
            _authorRepository = new Mock<IAuthorRepository>();
            IValidator<UpdateAuthorCommand> updateAuthorCommandValidator =
                new UpdateAuthorCommandValidator(_authorRepository.Object);

            _updateAuthorCommandHandler =
                new UpdateAuthorCommandHandler(updateAuthorCommandValidator, _authorRepository.Object);
        }

        [Fact]
        public void Should_Update_When_Command_Is_Valid()
        {
            //Arrange
            var author = new UpdateAuthorCommand { Id = Guid.NewGuid(), Name = "Robert Cecil Martin" };

            //Act
            _updateAuthorCommandHandler.Handle(author);

            //Assert
            _authorRepository.Verify(r => r.Update(It.IsAny<Domain.Entities.Author>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Update_When_Command_Is_Invalid()
        {
            //Arrange
            var author = new UpdateAuthorCommand { Name = "Robert Cecil Martin" };

            //Act
            _updateAuthorCommandHandler.Handle(author);

            //Assert
            _authorRepository.Verify(r => r.Update(It.IsAny<Domain.Entities.Author>()), Times.Never);
        }
    }
}
