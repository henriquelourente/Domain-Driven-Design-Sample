using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Commands.Author.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Domain.Tests.Commands.Author.Validators
{
    public class UpdateAuthorCommandValidatorTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { Guid.Empty, false },
                new object[] { Guid.NewGuid(), true }
            };
        
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly UpdateAuthorCommandValidator _updateAuthorCommandValidator;

        public UpdateAuthorCommandValidatorTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _updateAuthorCommandValidator = new UpdateAuthorCommandValidator(_authorRepositoryMock.Object);
        }

        
        [Theory]
        [MemberData(nameof(Data))]
        public void Id_Cant_Be_Empty(Guid id, bool result)
        {
            var command = new UpdateAuthorCommand{Id = id, Name = "Uncle Bob"};
            var validationResult = _updateAuthorCommandValidator.Validate(command);
            validationResult.IsValid.Should().Be(result);
        }

        [Theory]
        [InlineData("Robert Cecil Martin", true)]
        [InlineData("Martin Fowler", false)]
        public void Name_Cant_Be_Repeated(string name, bool exists)
        {
            //Arrange
            var UpdateAuthorCommand = new UpdateAuthorCommand { Id= Guid.NewGuid(), Name = name };
            _authorRepositoryMock.Setup(a => a.ExistsAsync(name)).Returns(Task.FromResult(exists));

            //Act
            var validationResults = _updateAuthorCommandValidator.Validate(UpdateAuthorCommand);

            //Assert
            validationResults.IsValid.Should().Be(!exists);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("J.R.R. Tolkien", true)]
        public void Name_Cant_Be_Null_Or_Empty(string name, bool isValid)
        {
            //Arrange
            var author = new UpdateAuthorCommand { Id = Guid.NewGuid(), Name = name };

            //Act
            var validationResult = _updateAuthorCommandValidator.Validate(author);

            //Assert
            validationResult.IsValid.Should().Be(isValid);
        }
    }
}