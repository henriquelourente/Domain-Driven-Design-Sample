using System;
using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Book.Validators
{
    public class UpdateBookCommandValidator: BookCommandValidatorBase<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(IBookRepository bookRepository, IValidator<PublicationCommand> publicationValidator) 
            : base(bookRepository, publicationValidator)
        {
            ValidateId();
        }

        private void ValidateId()
        {
            RuleFor(updateBookCommand => updateBookCommand.Id)
                .Must(id => !id.Equals(Guid.Empty))
                .WithSeverity(Severity.Error)
                .WithMessage("Id can't be empty");
        }
    }
}