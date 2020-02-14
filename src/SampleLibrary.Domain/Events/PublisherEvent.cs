using System;
using SampleLibrary.Core.Messages;

namespace SampleLibrary.Domain.Events
{
    public class PublisherEvent : Message
    {
        public string Name { get;  set; }
    }
}
