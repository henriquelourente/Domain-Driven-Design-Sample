using FluentValidation;
using SampleLibrary.Application.AutoMapper;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Author
{
    public class UpdateAuthorCommandHandler: CommandHandlerBase,  ICommandHandler<UpdateAuthorCommand>
    {
        private readonly IValidator<UpdateAuthorCommand> _createAuthorCommandValidator;
        private readonly IAuthorRepository _authorRepository;
        
        public UpdateAuthorCommandHandler(IValidator<UpdateAuthorCommand> validator, IAuthorRepository authorRepository)
        {
            _createAuthorCommandValidator = validator;
            _authorRepository = authorRepository;
        }

        public Result Handle(UpdateAuthorCommand command)
        {
            var validationResult = Validate(command, _createAuthorCommandValidator);

            if (validationResult.IsValid)
            {
                var author = Mapper<Domain.Entities.Author, UpdateAuthorCommand>.CommandToEntity(command);
                _authorRepository.Update(author);
                _authorRepository.SaveChanges();
            }

            return Return();
        }
    }
}