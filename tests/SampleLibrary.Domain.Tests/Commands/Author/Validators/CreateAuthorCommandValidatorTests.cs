using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Commands.Author.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Domain.Tests.Commands.Author.Validators
{
    public class CreateAuthorCommandValidatorTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly CreateAuthorCommandValidator _createAuthorCommandValidator;

        public CreateAuthorCommandValidatorTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _createAuthorCommandValidator = new CreateAuthorCommandValidator(_authorRepositoryMock.Object);
        }

        [Theory]
        [InlineData("Robert Cecil Martin", true)]
        [InlineData("Martin Fowler", false)]
        public void Name_Cant_Be_Repeated(string name, bool exists)
        {
            //Arrange
            var createAuthorCommand = new CreateAuthorCommand {Name = name};
            _authorRepositoryMock.Setup(a => a.Exists(name)).Returns(Task.FromResult(exists));

            //Act
            var validationResults = _createAuthorCommandValidator.Validate(createAuthorCommand);

            //Assert
            validationResults.IsValid.Should().Be(!exists);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("J.R.R. Tolkien", true)]
        public void Name_Cant_Be_Null_Or_Empty(string name, bool isValid)
        {
            //Arrange
            var author = new CreateAuthorCommand {Name = name};

            //Act
            var validationResult = _createAuthorCommandValidator.Validate(author);

            //Assert
            validationResult.IsValid.Should().Be(isValid);
        }
    }
}