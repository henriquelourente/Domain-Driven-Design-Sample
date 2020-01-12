using System;
using FluentAssertions;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects;
using SampleLibrary.Domain.Tests.Entities.Validators.Validators;
using Xunit;

namespace SampleLibrary.Domain.Tests.Entities.Validators
{
    public class BookValidatorTests
    {
        private readonly BookValidator _bookValidator;
        private readonly Guid _authorId;
        private readonly Guid _publisherId;
        private readonly Publication _publication;
        private readonly string _title;

        public BookValidatorTests()
        {
            _bookValidator = new BookValidator();
            _authorId = Guid.NewGuid();
            _publisherId = Guid.NewGuid();
            _publication = new Publication(1, DateTime.Today.Year);
            _title = "Clean Code";
        }

        [Fact]
        public void Correct_Parameters_Should_Be_Valid()
        {
            var book = new Book(_title, _authorId, _publisherId, _publication);
            
            var validationResults = _bookValidator.Validate(book);
            
            validationResults.IsValid.Should().BeTrue();
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Title_Cant_Be_Null_Or_Empty(string title)
        {
            var book = new Book(title, _authorId, _publisherId, _publication);
            
            var validationResults = _bookValidator.Validate(book);
            
            validationResults.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Author_Cant_Be_Empty()
        {
            var book = new Book(_title, Guid.Empty, _publisherId, _publication);
            
            var validationResults = _bookValidator.Validate(book);
            
            validationResults.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void Publisher_Cant_Be_Empty()
        {
            var book = new Book(_title, _authorId, Guid.Empty, _publication);
            
            var validationResults = _bookValidator.Validate(book);
            
            validationResults.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void Publication_Should_Be_Valid()
        {
            var publication = new Publication(1, DateTime.Today.AddYears(1).Year);
            var book = new Book(_title, _authorId, _publisherId, publication);
            
            var validationResults = _bookValidator.Validate(book);
            
            validationResults.IsValid.Should().BeFalse();
        }
    }
}