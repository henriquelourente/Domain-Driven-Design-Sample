using SampleLibrary.Core.Messages;
using SampleLibrary.Domain.Entities.ValueObjects;

namespace SampleLibrary.Domain.Events
{
    public class BookEvent : Message
    {
        public string Title { get; set; }
        public Publication Publication { get;  set; }
        public virtual AuthorEvent Author { get;  set; }
        public virtual PublisherEvent Publisher { get;  set; }
    }
}
