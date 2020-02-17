using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentValidation;
using Moq;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Book.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Domain.Tests.Commands.Book.Validators
{
    public class DeleteBookCommandValidatorTests
    {
        private IValidator<DeleteBookCommand> _deleteBookCommandValidator;
        private readonly Guid _bookId = Guid.NewGuid();

        public DeleteBookCommandValidatorTests()
        {
            var book = new Domain.Entities.Book("Clean Code", Guid.NewGuid(), Guid.NewGuid(), null);

            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(r => r.GetById(_bookId)).Returns(book);

            _deleteBookCommandValidator = new DeleteBookCommandValidator(bookRepository.Object);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Book_Must_Exist(Domain.Entities.Book book, bool result)
        {
            //Assert
            var deleteBookCommand = new DeleteBookCommand { Id = _bookId };

            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(r => r.GetById(_bookId)).Returns(book);

            _deleteBookCommandValidator = new DeleteBookCommandValidator(bookRepository.Object);

            //Act
            var validationResult = _deleteBookCommandValidator.Validate(deleteBookCommand);

            //Assert
            validationResult.IsValid.Should().Be(result);
        }

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { new Domain.Entities.Book("Clean Code", Guid.NewGuid(), Guid.NewGuid(), null), true };
            yield return new object[] { null, false };
        }
    }
}
