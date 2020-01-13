using System;

namespace SampleLibrary.Domain.Commands.Publisher
{
    public class UpdatePublisherCommand : PublisherCommandBase
    {
        public Guid Id { get; set; }
    }
}