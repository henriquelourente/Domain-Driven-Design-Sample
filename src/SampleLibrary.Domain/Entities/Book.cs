using System;
using SampleLibrary.Core.Entity;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects;

namespace SampleLibrary.Domain.Entities
{
    public class Book : Entity, IAgregateRoot
    {
        public string Title { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid PublisherId { get; private set; }
        public Publication Publication { get; private set; }
        public virtual Author Author { get; private set; }
        public virtual Publisher Publisher { get; private set; }

        public Book(string title, Guid authorId, Guid publisherId, Publication publication)
        {
            Title = title;
            AuthorId = authorId;
            PublisherId = publisherId;
            Publication = publication;
        }

        protected Book()
        {
            
        }
    }
}
