using SampleLibrary.Application.Author;
using SampleLibrary.Application.Publisher;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects;

namespace SampleLibrary.Application.Book
{
    public class BookEvent : CommandBase, IMessage
    {
        public string Title { get; private set; }
        public Publication Publication { get; private set; }
        public virtual AuthorEvent Author { get; private set; }
        public virtual PublisherEvent Publisher { get; private set; }
    }
}
