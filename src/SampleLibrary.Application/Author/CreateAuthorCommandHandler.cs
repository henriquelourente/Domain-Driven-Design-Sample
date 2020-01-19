using FluentValidation;
using SampleLibrary.Application.AutoMapper;
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

        public CreateAuthorCommandHandler(IValidator<CreateAuthorCommand> validator, IAuthorRepository authorRepository)
        {
            _createAuthorCommandValidator = validator;
            _authorRepository = authorRepository;
        }

        public Result Handle(CreateAuthorCommand command)
        {
            var validationResult = Validate(command, _createAuthorCommandValidator);

            if (validationResult.IsValid)
            {
                var author = Mapper<Domain.Entities.Author, CreateAuthorCommand>.CommandToEntity(command);
                _authorRepository.Add(author);
                _authorRepository.Commit();
            }

            return Return();
        }
    }
}