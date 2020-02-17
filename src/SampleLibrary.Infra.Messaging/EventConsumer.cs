using EasyNetQ;
using SampleLibrary.Core.Interfaces;

namespace SampleLibrary.Infra.Messaging
{
    public abstract class EventConsumer<TMessage, TPrimaryKey> :
        IEventConsumer<TMessage, TPrimaryKey>
        where TMessage : class, Core.Messages.IMessage<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected readonly IElasticsearchRepository<TMessage, TPrimaryKey> _elasticsearchRepository;
        protected IBus _bus;

        protected EventConsumer(
            IElasticsearchRepository<TMessage, TPrimaryKey> elasticsearchRepository)
        {
            _elasticsearchRepository = elasticsearchRepository;
        }

        public virtual void Subscribe()
        {
            _bus = RabbitHutch.CreateBus("host=localhost;virtualhost=sample-library;username=guest;password=guest");
            _bus.Subscribe<TMessage>("Books", HandleMessage);
        }

        public virtual void Unsubscribe() =>_bus.Dispose();

        protected virtual void HandleMessage(TMessage message) => _elasticsearchRepository.AddOrUpdateAsync(message).Wait();
    }
}
