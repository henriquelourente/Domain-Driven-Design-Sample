namespace SampleLibrary.Core.Interfaces
{
    public interface IEventPublisher
    {
        void Publish(IMessage message);
    }
}
