using SampleLibrary.Core.Commands;

namespace SampleLibrary.Domain.Commands.Author
{
    public abstract class AuthorCommandBase : Command
    {
        public string Name { get;  set; }
    }
}