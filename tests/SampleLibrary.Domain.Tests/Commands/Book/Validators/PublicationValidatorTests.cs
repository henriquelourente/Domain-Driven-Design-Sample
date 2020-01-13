using System;
using FluentAssertions;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Book.Validators;
using Xunit;

namespace SampleLibrary.Domain.Tests.Commands.Book.Validators
{
    public class PublicationValidatorTests
    {
        private readonly PublicationCommandValidator _publicationValidator;

        public PublicationValidatorTests()
        {
            _publicationValidator = new PublicationCommandValidator();
        }

        [Fact]
        public void Publication_Year_Cant_Be_Greater_Than_Current_Year()
        {
            var publication = new PublicationCommand {Edition = 1, Year = DateTime.Today.AddYears(1).Year};

            var validationResult = _publicationValidator.Validate(publication);

            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Publication_Year_Must_Be_Equal_Or_Lower_Than_Current_Year()
        {
            var publication = new PublicationCommand {Edition = 1, Year = DateTime.Today.Year};

            var validationResult = _publicationValidator.Validate(publication);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Publication_Cant_Be_Lower_Or_Equal_To_Zero()
        {
            var publication = new PublicationCommand {Edition = 0, Year = DateTime.Today.Year};

            var validationResult = _publicationValidator.Validate(publication);

            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Publication_Edition_Must_Be_Greater_Than_Zero()
        {
            var publication = new PublicationCommand {Edition = 1, Year = DateTime.Today.Year};

            var validationResult = _publicationValidator.Validate(publication);

            validationResult.IsValid.Should().BeTrue();
        }
    }
}