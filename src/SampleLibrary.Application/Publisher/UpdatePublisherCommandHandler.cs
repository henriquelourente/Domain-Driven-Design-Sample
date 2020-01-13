using FluentValidation;
using SampleLibrary.Application.AutoMapper;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Publisher
{
    public class UpdatePublisherCommandHandler : ICommandHandler<UpdatePublisherCommand>
    {
        private readonly IValidator<UpdatePublisherCommand> _updatePublisherCommandValidator;
        private readonly IPublisherRepository _publisherRepository;

        public UpdatePublisherCommandHandler(IValidator<UpdatePublisherCommand> validator,
            IPublisherRepository publisherRepository)
        {
            _updatePublisherCommandValidator = validator;
            _publisherRepository = publisherRepository;
        }

        public void Handle(UpdatePublisherCommand command)
        {
            var validationResult = _updatePublisherCommandValidator.Validate(command);

            if (!validationResult.IsValid)
                return;

            var publisher = Mapper<Domain.Entities.Publisher, UpdatePublisherCommand>.CommandToEntity(command);
            _publisherRepository.Update(publisher);
            _publisherRepository.Commit();
        }
    }
}