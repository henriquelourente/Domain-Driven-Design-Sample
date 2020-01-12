using SampleLibrary.Core.Entity;

namespace SampleLibrary.Domain.Entities
{
    public class Publisher : Entity, IAgregateRoot
    {
        public Publisher(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}