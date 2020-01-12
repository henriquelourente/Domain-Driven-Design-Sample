using FluentAssertions;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities;
using SampleLibrary.Domain.Tests.Entities.Validators.Validators;
using Xunit;

namespace SampleLibrary.Domain.Tests.Entities.Validators.Tests.Validators
{
    public class PublisherValidatorTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("Pearson", true)]
        public void Name_Cant_Be_Null_Or_Empty(string name, bool isValid)
        {
            var publisher = new Publisher(name);
            var publisherValidator = new PublisherValidator();

            var validationResult = publisherValidator.Validate(publisher);

            validationResult.IsValid.Should().Be(isValid);
        }
    }
}