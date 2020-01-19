using SampleLibrary.Core.Commands;

namespace SampleLibrary.Domain.Commands.Publisher
{
    public abstract class PublisherCommandBase : CommandBase
    {
        public string Name { get; set; }
    }
}