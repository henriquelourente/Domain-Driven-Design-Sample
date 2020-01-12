using SampleLibrary.Core.Entity;

namespace SampleLibrary.Domain.Entities
{
    public class Author: Entity, IAgregateRoot
    {
        public Author(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}