using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Book.Validators
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            ValidateExists();
        }

        private void ValidateExists()
        {
            RuleFor(bookBaseCommand => bookBaseCommand.Id)
                .Must(id => _bookRepository.GetById(id) != null)
                .WithSeverity(Severity.Error)
                .WithMessage("Book not exists.");
        }
    }
}