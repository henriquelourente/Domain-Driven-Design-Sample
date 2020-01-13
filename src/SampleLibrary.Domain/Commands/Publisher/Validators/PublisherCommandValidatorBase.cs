using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Publisher.Validators
{
    public abstract class PublisherCommandValidatorBase<T>: AbstractValidator<T> where T : PublisherCommandBase
    {
        private readonly IPublisherRepository _publisherRepository;

        protected PublisherCommandValidatorBase(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
            ValidateNameIsUnique();
            ValidateName();
        }
        
        private void ValidateNameIsUnique()
        {
            RuleFor(publisherBaseCommand => publisherBaseCommand.Name)
                .MustAsync(async (name, cancellationToken) => !(await _publisherRepository.Exists(name)))
                .WithSeverity(Severity.Error)
                .WithMessage("A publisher with this name already exists.");
        }
        
        private void ValidateName()
        {
            RuleFor(publisherBaseCommand => publisherBaseCommand.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithSeverity(Severity.Error)
                .WithMessage("Name can't be empty");
        }
    }
}