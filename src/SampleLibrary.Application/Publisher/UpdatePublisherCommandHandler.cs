using FluentValidation;
using SampleLibrary.Application.Mapping;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Publisher
{
    public class UpdatePublisherCommandHandler : CommandHandlerBase, ICommandHandler<UpdatePublisherCommand>
    {
        private readonly IValidator<UpdatePublisherCommand> _updatePublisherCommandValidator;
        private readonly IPublisherRepository _publisherRepository;
        private readonly AppForger _forger;

        public UpdatePublisherCommandHandler(IValidator<UpdatePublisherCommand> validator,
            IPublisherRepository publisherRepository,
            AppForger forger)
        {
            _updatePublisherCommandValidator = validator;
            _publisherRepository = publisherRepository;
            _forger = forger;
        }

        public Result Handle(UpdatePublisherCommand command)
        {
            var validationResult = Validate(command, _updatePublisherCommandValidator);

            if (validationResult.IsValid)
            {
                var publisher = _forger.Forge(command);
                _publisherRepository.Update(publisher);
                _publisherRepository.SaveChanges();
            }

            return Return();
        }
    }
}
