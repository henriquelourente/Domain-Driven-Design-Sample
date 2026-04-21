using FluentValidation;
using SampleLibrary.Application.Mapping;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Publisher
{
    public class CreatePublisherCommandHandler : CommandHandlerBase, ICommandHandler<CreatePublisherCommand>
    {
        private readonly IValidator<CreatePublisherCommand> _createPublisherCommandValidator;
        private readonly IPublisherRepository _publisherRepository;
        private readonly AppForger _forger;

        public CreatePublisherCommandHandler(IValidator<CreatePublisherCommand> validator,
            IPublisherRepository publisherRepository,
            AppForger forger)
        {
            _createPublisherCommandValidator = validator;
            _publisherRepository = publisherRepository;
            _forger = forger;
        }

        public Result Handle(CreatePublisherCommand command)
        {
            var validationResult = Validate(command, _createPublisherCommandValidator);

            if (validationResult.IsValid)
            {
                var publisher = _forger.Forge(command);
                _publisherRepository.Add(publisher);
                _publisherRepository.SaveChanges();
            }

            return Return();
        }
    }
}
