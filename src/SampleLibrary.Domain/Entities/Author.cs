using System.Collections.Generic;
using SampleLibrary.Core.Entity;

namespace SampleLibrary.Domain.Entities
{
    public class Author : Entity, IAgregateRoot
    {
        public Author(string name)
        {
            Name = name;
            Books = new List<Book>();
        }

        public string Name { get; private set; }
        public virtual IEnumerable<Book> Books { get; private set; }
    }
}