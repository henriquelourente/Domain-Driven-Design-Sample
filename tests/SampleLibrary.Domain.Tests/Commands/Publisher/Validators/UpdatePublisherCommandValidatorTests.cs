using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Commands.Publisher.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Domain.Tests.Commands.Publisher.Validators
{
    public class UpdatePublisherCommandValidatorTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {Guid.Empty, false},
                new object[] {Guid.NewGuid(), true}
            };

        private readonly Mock<IPublisherRepository> _publisherRepositoryMock;
        private readonly UpdatePublisherCommandValidator _updatePublisherCommandValidator;

        public UpdatePublisherCommandValidatorTests()
        {
            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _updatePublisherCommandValidator = new UpdatePublisherCommandValidator(_publisherRepositoryMock.Object);
        }


        [Theory]
        [MemberData(nameof(Data))]
        public void Id_Cant_Be_Empty(Guid id, bool result)
        {
            var command = new UpdatePublisherCommand {Id = id, Name = "Uncle Bob"};
            var validationResult = _updatePublisherCommandValidator.Validate(command);
            validationResult.IsValid.Should().Be(result);
        }

        [Theory]
        [InlineData("Pearson", true)]
        [InlineData("O'Reilly", false)]
        public void Name_Cant_Be_Repeated(string name, bool exists)
        {
            //Arrange
            var updatePublisherCommand = new UpdatePublisherCommand {Id = Guid.NewGuid(), Name = name};
            _publisherRepositoryMock.Setup(a => a.Exists(name)).Returns(Task.FromResult(exists));

            //Act
            var validationResults = _updatePublisherCommandValidator.Validate(updatePublisherCommand);

            //Assert
            validationResults.IsValid.Should().Be(!exists);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("Pearson", true)]
        public void Name_Cant_Be_Null_Or_Empty(string name, bool isValid)
        {
            //Arrange
            var publisher = new UpdatePublisherCommand {Id = Guid.NewGuid(), Name = name};

            //Act
            var validationResult = _updatePublisherCommandValidator.Validate(publisher);

            //Assert
            validationResult.IsValid.Should().Be(isValid);
        }
    }
}