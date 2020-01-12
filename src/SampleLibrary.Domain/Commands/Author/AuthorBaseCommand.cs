using SampleLibrary.Core.Commands;

namespace SampleLibrary.Domain.Commands.Author
{
    public abstract class AuthorBaseCommand : Command
    {
        public string Name { get;  set; }
    }
}