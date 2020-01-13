using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects;

namespace SampleLibrary.Domain.Commands.Book.Validators
{
    public class CreateBookCommandValidator : BookCommandValidatorBase<CreateBookCommand>
    {
        public CreateBookCommandValidator(IBookRepository bookRepository, IValidator<PublicationCommand> publicationValidator) 
            : base(bookRepository, publicationValidator)
        {
        }
    }
}