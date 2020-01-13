using SampleLibrary.Core.Commands;

namespace SampleLibrary.Domain.Commands.Publisher
{
    public abstract class PublisherCommandBase : Command
    {
        public string Name { get; set; }
    }
}