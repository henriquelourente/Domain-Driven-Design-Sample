using SampleLibrary.Core.Messages;

namespace SampleLibrary.Domain.Events
{
    public class AuthorEvent : Message
    {
        public string Name { get; set; }
    }
}
