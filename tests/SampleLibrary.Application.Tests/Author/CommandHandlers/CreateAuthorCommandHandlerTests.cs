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
    public class CreateAuthorCommandHandlerTests
    {
        private readonly CreateAuthorCommandHandler _createAuthorCommandHandler;
        private readonly Mock<IAuthorRepository> _authorRepository;

        public CreateAuthorCommandHandlerTests()
        {
            _authorRepository = new Mock<IAuthorRepository>();
            IValidator<CreateAuthorCommand> createAuthorCommandValidator =
                new CreateAuthorCommandValidator(_authorRepository.Object);

            _createAuthorCommandHandler =
                new CreateAuthorCommandHandler(createAuthorCommandValidator, _authorRepository.Object);
        }

        [Fact]
        public void Should_Create_When_Command_Is_Valid()
        {
            //Arrange
            var author = new CreateAuthorCommand {Name = "Robert Cecil Martin"};

            //Act
            _createAuthorCommandHandler.Handle(author);

            //Assert
            _authorRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Author>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Create_When_Command_Is_Invalid()
        {
            //Arrange
            var author = new CreateAuthorCommand {Name = String.Empty};

            //Act
            _createAuthorCommandHandler.Handle(author);

            //Assert
            _authorRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Author>()), Times.Never);
        }
    }
}