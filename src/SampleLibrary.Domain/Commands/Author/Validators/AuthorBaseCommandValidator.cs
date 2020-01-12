using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Author.Validators
{
    public abstract class AuthorBaseCommandValidator<T>: AbstractValidator<T> where T : AuthorBaseCommand
    {
        private readonly IAuthorRepository _authorRepository;

        protected AuthorBaseCommandValidator(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            ValidateNameIsUnique();
            ValidateName();
        }
        
        private void ValidateNameIsUnique()
        {
            RuleFor(authorBaseCommand => authorBaseCommand.Name)
                .MustAsync(async (name, cancellationToken) => !(await _authorRepository.Exists(name)))
                .WithSeverity(Severity.Error)
                .WithMessage("A author with this name already exists.");
        }
        
        private void ValidateName()
        {
            RuleFor(authorBaseCommand => authorBaseCommand.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithSeverity(Severity.Error)
                .WithMessage("Name can't be empty");
        }
    }
}