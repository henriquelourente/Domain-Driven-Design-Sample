using System;
using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Book.Validators
{
    public abstract class BookCommandValidatorBase<T> : AbstractValidator<T> where T : BookCommandBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<PublicationCommand> _publicationValidator;

        protected BookCommandValidatorBase(IBookRepository bookRepository, IValidator<PublicationCommand> publicationValidator)
        {
            _bookRepository = bookRepository;
            _publicationValidator = publicationValidator;

            ValidateTitle();
            ValidateAuthor();
            ValidatePublisher();
            ValidatePublication();
        }

        protected void ValidateNameIsUniqueOnCreate()
        {
            RuleFor(bookBaseCommand => bookBaseCommand.Title)
                .MustAsync(async (title, cancellationToken) => !(await _bookRepository.ExistsAsync(title)))
                .WithSeverity(Severity.Error)
                .WithMessage("A book with this name already exists.");
        }

        private void ValidateTitle()
        {
            RuleFor(bookBaseCommand => bookBaseCommand.Title)
                .Must(title => !string.IsNullOrWhiteSpace(title))
                .WithSeverity(Severity.Error)
                .WithMessage("Title can't be empty");
        }

        private void ValidateAuthor()
        {
            RuleFor(bookBaseCommand => bookBaseCommand.AuthorId)
                .Must(authorId => !Guid.Empty.Equals(authorId))
                .WithSeverity(Severity.Error)
                .WithMessage("Author can't be empty");
        }

        private void ValidatePublisher()
        {
            RuleFor(bookBaseCommand => bookBaseCommand.PublisherId)
                .Must(publisherId => !Guid.Empty.Equals(publisherId))
                .WithSeverity(Severity.Error)
                .WithMessage("Publisher can't be empty");
        }

        private void ValidatePublication()
        {
            RuleFor(bookBaseCommand => bookBaseCommand.Publication)
                .Must(publication => _publicationValidator.Validate(publication).IsValid)
                .WithSeverity(Severity.Error)
                .WithMessage("Publication is invalid");
        }
    }
}