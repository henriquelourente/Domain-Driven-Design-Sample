using FluentValidation;
using SampleLibrary.Application.Mapping;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Author
{
    public class CreateAuthorCommandHandler : CommandHandlerBase, ICommandHandler<CreateAuthorCommand>
    {
        private readonly IValidator<CreateAuthorCommand> _createAuthorCommandValidator;
        private readonly IAuthorRepository _authorRepository;
        private readonly AppForger _forger;

        public CreateAuthorCommandHandler(IValidator<CreateAuthorCommand> validator, IAuthorRepository authorRepository, AppForger forger)
        {
            _createAuthorCommandValidator = validator;
            _authorRepository = authorRepository;
            _forger = forger;
        }

        public Result Handle(CreateAuthorCommand command)
        {
            var validationResult = Validate(command, _createAuthorCommandValidator);

            if (validationResult.IsValid)
            {
                var author = _forger.Forge(command);
                _authorRepository.Add(author);
                _authorRepository.SaveChanges();
            }

            return Return();
        }
    }
}
