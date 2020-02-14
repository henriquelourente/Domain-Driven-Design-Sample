using EasyNetQ;
using SampleLibrary.Core.Interfaces;

namespace SampleLibrary.Infra.Messaging
{
    public class EventPublisher<TMessage> : IEventPublisher<TMessage> where TMessage : class
    {
        public void Publish(TMessage message)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualhost=sample-library;username=guest;password=guest"))
            {
                bus.Publish(message);
            }
        }
    }
}
