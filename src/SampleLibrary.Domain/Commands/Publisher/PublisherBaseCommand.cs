using SampleLibrary.Core.Commands;

namespace SampleLibrary.Domain.Commands.Publisher
{
    public abstract class PublisherBaseCommand : Command
    {
        public string Name { get; set; }
    }
}