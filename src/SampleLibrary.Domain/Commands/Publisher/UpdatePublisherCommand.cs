using System;

namespace SampleLibrary.Domain.Commands.Publisher
{
    public class UpdatePublisherCommand : PublisherBaseCommand
    {
        public Guid Id { get; set; }
    }
}