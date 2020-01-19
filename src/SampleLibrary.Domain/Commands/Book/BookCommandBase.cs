using System;
using SampleLibrary.Core.Commands;


namespace SampleLibrary.Domain.Commands.Book
{
    public abstract class BookCommandBase : CommandBase
    {
        public string Title { get; set; }
        public PublicationCommand Publication { get; set; }
        public Guid PublisherId { get; set; }
        public Guid AuthorId { get; set; }
    }
    
    public class PublicationCommand
    {
        public int Edition { get; set; }
        public int Year { get; set; }

    }
}