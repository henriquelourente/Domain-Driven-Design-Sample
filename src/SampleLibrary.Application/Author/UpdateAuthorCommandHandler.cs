using FluentValidation;
using SampleLibrary.Application.Mapping;
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
        private readonly AppForger _forger;

        public UpdateAuthorCommandHandler(IValidator<UpdateAuthorCommand> validator, IAuthorRepository authorRepository, AppForger forger)
        {
            _createAuthorCommandValidator = validator;
            _authorRepository = authorRepository;
            _forger = forger;
        }

        public Result Handle(UpdateAuthorCommand command)
        {
            var validationResult = Validate(command, _createAuthorCommandValidator);

            if (validationResult.IsValid)
            {
                var author = _forger.Forge(command);
                _authorRepository.Update(author);
                _authorRepository.SaveChanges();
            }

            return Return();
        }
    }
}
