using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Domain.Commands.Author.Validators
{
    public class CreateAuthorCommandValidator : AuthorBaseCommandValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator(IAuthorRepository authorRepository)
            : base((authorRepository))
        {
        }
    }
}