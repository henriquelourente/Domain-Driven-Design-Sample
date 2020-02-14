using System;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Events;

namespace SampleLibrary.IoC
{
    public class ConsumerSubscriptions : IConsumerSubscriptions
    {
        private readonly IEventConsumer<BookEvent, Guid> _bookEventConsumer;

        public ConsumerSubscriptions(IEventConsumer<BookEvent, Guid> bookEventConsumer)
        {
            _bookEventConsumer = bookEventConsumer;
        }

        public void Subscribe()
        {
            _bookEventConsumer.Subscribe();
        }

        public void Unsubscribe()
        {
            _bookEventConsumer.Unsubscribe();
        }
    }

    public interface IConsumerSubscriptions
    {
        void Subscribe();
        void Unsubscribe();
    }
}
