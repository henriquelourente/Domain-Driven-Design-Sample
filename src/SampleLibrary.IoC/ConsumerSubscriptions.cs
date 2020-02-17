using System;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Events;

namespace SampleLibrary.IoC
{
    public class ConsumerSubscriptions : IConsumerSubscriptions
    {
        private readonly IEventConsumer<BookEvent, Guid> _bookEventConsumer;
        private readonly IEventConsumer<DeleteBookEvent, Guid> _deleteBookEventConsumer;

        public ConsumerSubscriptions(
            IEventConsumer<BookEvent, Guid> bookEventConsumer,
            IEventConsumer<DeleteBookEvent, Guid> deleteBookEventConsumer)
        {
            _bookEventConsumer = bookEventConsumer;
            _deleteBookEventConsumer = deleteBookEventConsumer;
        }

        public void Subscribe()
        {
            _bookEventConsumer.Subscribe();
            _deleteBookEventConsumer.Subscribe();
        }

        public void Unsubscribe()
        {
            _bookEventConsumer.Unsubscribe();
            _deleteBookEventConsumer.Unsubscribe();
        }
    }

    public interface IConsumerSubscriptions
    {
        void Subscribe();
        void Unsubscribe();
    }
}
