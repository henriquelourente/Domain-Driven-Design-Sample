using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Publisher.Validators
{
    public class CreatePublisherCommandValidator : PublisherCommandValidatorBase<CreatePublisherCommand>
    {
        public CreatePublisherCommandValidator(IPublisherRepository publisherRepository)
            : base(publisherRepository)
        {
        }
    }
}