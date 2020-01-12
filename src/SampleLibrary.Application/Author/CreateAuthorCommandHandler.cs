using FluentValidation;
using SampleLibrary.Application.Author.Mappers;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Author
{
    public class CreateAuthorCommandHandler : ICommandHandler<CreateAuthorCommand>
    {
        private readonly IValidator<CreateAuthorCommand> _createAuthorCommandValidator;
        private readonly IAuthorRepository _authorRepository;
        
        public CreateAuthorCommandHandler(IValidator<CreateAuthorCommand> validator, IAuthorRepository authorRepository)
        {
            _createAuthorCommandValidator = validator;
            _authorRepository = authorRepository;
        }

        public void Handle(CreateAuthorCommand command)
        {
            var validationResult = _createAuthorCommandValidator.Validate(command);

            if (!validationResult.IsValid) 
                return;
            
            var author = Mapper<Domain.Entities.Author, CreateAuthorCommand>.CommandToEntity(command);
            _authorRepository.Add(author);
            _authorRepository.Commit();
        }
    }
}