using FluentValidation;
using SampleLibrary.Application.AutoMapper;
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

        public UpdatePublisherCommandHandler(IValidator<UpdatePublisherCommand> validator,
            IPublisherRepository publisherRepository)
        {
            _updatePublisherCommandValidator = validator;
            _publisherRepository = publisherRepository;
        }

        public Result Handle(UpdatePublisherCommand command)
        {
            var validationResult = Validate(command, _updatePublisherCommandValidator);

            if (validationResult.IsValid)
            {
                var publisher = Mapper<Domain.Entities.Publisher, UpdatePublisherCommand>.CommandToEntity(command);
                _publisherRepository.Update(publisher);
                _publisherRepository.Commit();
            }

            return Return();
        }
    }
}