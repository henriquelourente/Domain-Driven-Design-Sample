using System;
using SampleLibrary.Domain.Events;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Infra.Messaging
{
    public class BookEventConsumer : EventConsumer<BookEvent, Guid>
    {
        public BookEventConsumer(IBookEventRepository elasticsearchRepository)
            : base(elasticsearchRepository)
        {
            
        }
    }
}
