using Microsoft.Extensions.Hosting;
using SampleLibrary.Core.Messages;

namespace SampleLibrary.Core.Interfaces
{
    public interface IEventConsumer<TMessage, TPrimaryKey>
        where TMessage : class, IMessage<TPrimaryKey>
    {
        void Subscribe();
        void Unsubscribe();
    }
}
