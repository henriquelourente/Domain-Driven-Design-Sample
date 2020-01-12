using FluentValidation;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities;

namespace SampleLibrary.Domain.Tests.Entities.Validators.Validators
{
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator()
        {
            ValidateName();
        }

        private void ValidateName()
        {
            RuleFor(publisher => publisher.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithSeverity(Severity.Error)
                .WithMessage("Name can't be empty");
        }
    }
}