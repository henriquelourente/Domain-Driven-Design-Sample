using System;
using System.Threading;
using FluentValidation;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities;

namespace SampleLibrary.Domain.Tests.Entities.Validators.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            ValidateTitle();
            ValidateAuthor();
            ValidatePublisher();
            ValidatePublication();
        }

        private void ValidatePublication()
        {
            RuleFor(book => book.Publication)
                .Must(publication =>
                {
                    var publicationValidator = new PublicationValidator();
                    return publicationValidator.Validate(publication).IsValid;
                })
                .WithSeverity(Severity.Error)
                .WithMessage("Invalid publication. Verify edition and year.");
        }

        private void ValidatePublisher()
        {
            RuleFor(book => book.PublisherId)
                .Must(publisherId => publisherId != Guid.Empty)
                .WithSeverity(Severity.Error)
                .WithMessage("Publisher can't be empty;");
        }

        private void ValidateAuthor()
        {
            RuleFor(book => book.AuthorId)
                .Must(authorId => authorId != Guid.Empty)
                .WithSeverity(Severity.Error)
                .WithMessage("Author can't be empty;");
        }

        private void ValidateTitle()
        {
            RuleFor(book => book.Title)
                .Must(title => !string.IsNullOrWhiteSpace(title))
                .WithSeverity(Severity.Error)
                .WithMessage("Title can't be empty;");
        }


    }
}