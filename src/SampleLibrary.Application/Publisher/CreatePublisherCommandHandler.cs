using FluentValidation;
using SampleLibrary.Application.Author.Mappers;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Publisher
{
    public class CreatePublisherCommandHandler : ICommandHandler<CreatePublisherCommand>
    {
        private readonly IValidator<CreatePublisherCommand> _createPublisherCommandValidator;
        private readonly IPublisherRepository _publisherRepository;

        public CreatePublisherCommandHandler(IValidator<CreatePublisherCommand> validator,
            IPublisherRepository publisherRepository)
        {
            _createPublisherCommandValidator = validator;
            _publisherRepository = publisherRepository;
        }

        public void Handle(CreatePublisherCommand command)
        {
            var validationResult = _createPublisherCommandValidator.Validate(command);

            if (!validationResult.IsValid)
                return;

            var publisher = Mapper<Domain.Entities.Publisher, CreatePublisherCommand>.CommandToEntity(command);
            _publisherRepository.Add(publisher);
            _publisherRepository.Commit();
        }
    }
}