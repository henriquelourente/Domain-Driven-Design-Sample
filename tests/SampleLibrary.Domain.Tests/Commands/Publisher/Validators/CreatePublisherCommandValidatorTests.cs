using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Commands.Publisher.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Domain.Tests.Commands.Publisher.Validators
{
    public class CreatePublisherCommandValidatorTests
    {
        private readonly Mock<IPublisherRepository> _publisherRepositoryMock;
        private readonly CreatePublisherCommandValidator _createPublisherCommandValidator;

        public CreatePublisherCommandValidatorTests()
        {
            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _createPublisherCommandValidator = new CreatePublisherCommandValidator(_publisherRepositoryMock.Object);
        }

        [Theory]
        [InlineData("Pearson", true)]
        [InlineData("O'Reilly", false)]
        public void Name_Cant_Be_Repeated(string name, bool exists)
        {
            //Arrange
            var createPublisherCommand = new CreatePublisherCommand {Name = name};
            _publisherRepositoryMock.Setup(a => a.Exists(name)).Returns(Task.FromResult(exists));

            //Act
            var validationResults = _createPublisherCommandValidator.Validate(createPublisherCommand);

            //Assert
            validationResults.IsValid.Should().Be(!exists);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("Pearson", true)]
        public void Name_Cant_Be_Null_Or_Empty(string name, bool isValid)
        {
            //Arrange
            var publisher = new CreatePublisherCommand {Name = name};

            //Act
            var validationResult = _createPublisherCommandValidator.Validate(publisher);

            //Assert
            validationResult.IsValid.Should().Be(isValid);
        }
    }
}