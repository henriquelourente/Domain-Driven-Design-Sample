using System;
using FluentValidation;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Publisher.Validators
{
    public class UpdatePublisherCommandValidator : PublisherBaseCommandValidator<UpdatePublisherCommand>
    {
        public UpdatePublisherCommandValidator(IPublisherRepository publisherRepository)
            : base(publisherRepository)
        {
            ValidateId();
        }

        private void ValidateId()
        {
            RuleFor(updatePublisherCommand => updatePublisherCommand.Id)
                .Must(id => !id.Equals(Guid.Empty))
                .WithSeverity(Severity.Error)
                .WithMessage("Id can't be empty");
        }
    }
}