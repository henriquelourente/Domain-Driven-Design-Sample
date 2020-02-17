using FluentValidation;
using SampleLibrary.Application.AutoMapper;
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

        public CreatePublisherCommandHandler(IValidator<CreatePublisherCommand> validator,
            IPublisherRepository publisherRepository)
        {
            _createPublisherCommandValidator = validator;
            _publisherRepository = publisherRepository;
        }

        public Result Handle(CreatePublisherCommand command)
        {
            var validationResult = Validate(command, _createPublisherCommandValidator);

            if (validationResult.IsValid)
            {
                var publisher = Mapper<Domain.Entities.Publisher, CreatePublisherCommand>.CommandToEntity(command);
                _publisherRepository.Add(publisher);
                _publisherRepository.SaveChanges();
            }

            return Return();
        }
    }
}