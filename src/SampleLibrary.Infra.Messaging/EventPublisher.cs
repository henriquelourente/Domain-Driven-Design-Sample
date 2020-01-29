using EasyNetQ;
using SampleLibrary.Core.Interfaces;

namespace SampleLibrary.Infra.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        public void Publish(Core.Interfaces.IMessage message)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualhost=sample-library;username=guest;password=guest"))
            {
                bus.Publish(message);
            }
        }
    }
}
