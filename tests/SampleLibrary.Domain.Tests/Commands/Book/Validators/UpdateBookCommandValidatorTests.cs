using System;
using FluentAssertions;
using Moq;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Book.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Domain.Tests.Commands.Book.Validators
{
    public class UpdateBookCommandValidatorTests
    {
        private readonly UpdateBookCommandValidator _updateBookCommandValidator;
        private readonly PublicationCommand _publication;

        public UpdateBookCommandValidatorTests()
        {
            _publication = new PublicationCommand { Edition = 1, Year = DateTime.Today.Year };

            var bookRepository = new Mock<IBookRepository>();
            var publicationValidator = new PublicationCommandValidator();

            _updateBookCommandValidator =
                new UpdateBookCommandValidator(bookRepository.Object, publicationValidator);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("Clean Code", true)]
        public void Title_Cant_Be_Empty(string title, bool result)
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                Id = Guid.NewGuid(),
                Title = title,
                Publication = _publication,
                AuthorId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            //Act
            var validationResult = _updateBookCommandValidator.Validate(updateBookCommand);

            //Assert
            validationResult.IsValid.Should().Be(result);
        }

        [Fact]
        public void Publication_Must_Be_Valid()
        {
            //Arrange
            var publication = new PublicationCommand { Edition = 0, Year = DateTime.Today.Year };
            var updateBookCommand = new UpdateBookCommand
            {
                Id = Guid.NewGuid(),
                Title = "Clean Code",
                Publication = publication,
                AuthorId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            //Act
            var validationResult = _updateBookCommandValidator.Validate(updateBookCommand);

            //Assert
            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Author_Cant_Be_Empty()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                Id = Guid.NewGuid(),
                Title = "Clean Code",
                Publication = _publication,
                AuthorId = Guid.Empty,
                PublisherId = Guid.NewGuid()
            };

            //Act
            var validationResult = _updateBookCommandValidator.Validate(updateBookCommand);

            //Assert
            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Publisher_Cant_Be_Empty()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                Id = Guid.NewGuid(),
                Title = "Clean Code",
                Publication = _publication,
                AuthorId = Guid.NewGuid(),
                PublisherId = Guid.Empty
            };

            //Act
            var validationResult = _updateBookCommandValidator.Validate(updateBookCommand);

            //Assert
            validationResult.IsValid.Should().BeFalse();
        }
    }
}