using System;
using FluentAssertions;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects;
using SampleLibrary.Domain.Tests.Entities.Validators.Validators;
using Xunit;

namespace SampleLibrary.Domain.Tests.Entities.Validators.Tests.Validators
{
    public class PublicationValidatorTests
    {
        private readonly PublicationValidator _publicationValidator;

        public PublicationValidatorTests()
        {
            _publicationValidator = new PublicationValidator();
        }

        [Fact]
        public void Publication_Year_Cant_Be_Greater_Than_Current_Year()
        {
            var publication = new Publication(1, DateTime.Today.AddYears(1).Year);

            var validationResult = _publicationValidator.Validate(publication);
            
            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Publication_Year_Must_Be_Equal_Or_Lower_Than_Current_Year()
        {
            var publication = new Publication(1, DateTime.Today.Year);

            var validationResult = _publicationValidator.Validate(publication);
            
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Publication_Cant_Be_Lower_Or_Equal_To_Zero()
        {
            var publication = new Publication(0, DateTime.Today.Year);

            var validationResult = _publicationValidator.Validate(publication);
            
            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Publication_Edition_Must_Be_Greater_Than_Zero()
        {
            var publication = new Publication(1, DateTime.Today.Year);
            
            var validationResult = _publicationValidator.Validate(publication);
            
            validationResult.IsValid.Should().BeTrue();
        }
    }
}