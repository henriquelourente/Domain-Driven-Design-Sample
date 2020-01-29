using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Book.Validators
{
    public class CreateBookCommandValidator : BookCommandValidatorBase<CreateBookCommand>
    {
        public CreateBookCommandValidator(IBookRepository bookRepository, IValidator<PublicationCommand> publicationValidator) 
            : base(bookRepository, publicationValidator)
        {
            ValidateNameIsUniqueOnCreate();
        }
    }
}