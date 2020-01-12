using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Publisher.Validators
{
    public abstract class PublisherBaseCommandValidator<T>: AbstractValidator<T> where T : PublisherBaseCommand
    {
        private readonly IPublisherRepository _publisherRepository;

        protected PublisherBaseCommandValidator(IPublisherRepository publisherRepository)
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