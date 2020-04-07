using System.Collections.Generic;
using SampleLibrary.Core.Entity;

namespace SampleLibrary.Domain.Entities
{
    public class Publisher : Entity
    {
        public Publisher(string name)
        {
            Name = name;
            Books = new List<Book>();
        }

        public string Name { get; private set; }
        public virtual IEnumerable<Book> Books { get; private set; }
    }
}