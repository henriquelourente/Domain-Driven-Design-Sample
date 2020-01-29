using System;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;

namespace SampleLibrary.Application.Author
{
    public class AuthorEvent : CommandBase, IMessage
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
