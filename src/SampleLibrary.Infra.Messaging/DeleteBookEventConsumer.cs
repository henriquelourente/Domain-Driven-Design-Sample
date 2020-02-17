using System;
using System.Collections.Generic;
using System.Text;
using EasyNetQ;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Events;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Infra.Messaging
{
    public class DeleteBookEventConsumer : IEventConsumer<DeleteBookEvent, Guid>
    {
        private readonly IBookEventRepository _bookEventRepository;
        private IBus _bus;

        public DeleteBookEventConsumer(IBookEventRepository bookEventRepository)
        {
            _bookEventRepository = bookEventRepository;
        }

        public void Subscribe()
        {
            _bus = RabbitHutch.CreateBus("host=localhost;virtualhost=sample-library;username=guest;password=guest");
            _bus.Subscribe<DeleteBookEvent>(nameof(DeleteBookEvent), HandleMessage);
        }

        public  void Unsubscribe() => _bus.Dispose();

        protected  void HandleMessage(DeleteBookEvent message) => _bookEventRepository.RemoveAsync(message.Id).Wait();
    }
}
