using System;
using FluentAssertions;
using Moq;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Book.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Domain.Tests.Commands.Book.Validators
{
    public class CreateBookCommandValidatorTests
    {
        private readonly CreateBookCommandValidator _createBookCommandValidator;
        private readonly PublicationCommand _publication;

        public CreateBookCommandValidatorTests()
        {
            _publication = new PublicationCommand {Edition = 1, Year = DateTime.Today.Year};

            var bookRepository = new Mock<IBookRepository>();
            var publicationValidator = new PublicationCommandValidator();

            _createBookCommandValidator =
                new CreateBookCommandValidator(bookRepository.Object, publicationValidator);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("Clean Code", true)]
        public void Title_Cant_Be_Empty(string title, bool result)
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                Title = title, Publication = _publication, AuthorId = Guid.NewGuid(), PublisherId = Guid.NewGuid()
            };

            //Act
            var validationResult = _createBookCommandValidator.Validate(createBookCommand);

            //Assert
            validationResult.IsValid.Should().Be(result);
        }

        [Fact]
        public void Publication_Must_Be_Valid()
        {
            //Arrange
            var publication = new PublicationCommand {Edition = 0, Year = DateTime.Today.Year};
            var createBookCommand = new CreateBookCommand
            {
                Title = "Clean Code", Publication = publication, AuthorId = Guid.NewGuid(), PublisherId = Guid.NewGuid()
            };

            //Act
            var validationResult = _createBookCommandValidator.Validate(createBookCommand);

            //Assert
            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Author_Cant_Be_Empty()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                Title = "Clean Code", Publication = _publication, AuthorId = Guid.Empty, PublisherId = Guid.NewGuid()
            };

            //Act
            var validationResult = _createBookCommandValidator.Validate(createBookCommand);

            //Assert
            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Publisher_Cant_Be_Empty()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                Title = "Clean Code", Publication = _publication, AuthorId = Guid.NewGuid(), PublisherId = Guid.Empty
            };

            //Act
            var validationResult = _createBookCommandValidator.Validate(createBookCommand);

            //Assert
            validationResult.IsValid.Should().BeFalse();
        }
    }
}