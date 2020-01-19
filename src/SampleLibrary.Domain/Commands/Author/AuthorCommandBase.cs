using SampleLibrary.Core.Commands;

namespace SampleLibrary.Domain.Commands.Author
{
    public abstract class AuthorCommandBase : CommandBase
    {
        public string Name { get;  set; }
    }
}