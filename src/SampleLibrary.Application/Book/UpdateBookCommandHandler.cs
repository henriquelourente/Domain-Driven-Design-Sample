using FluentValidation;
using SampleLibrary.Application.AutoMapper;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Events;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Book
{
    public class UpdateBookCommandHandler : CommandHandlerBase, ICommandHandler<UpdateBookCommand>
    {
        private readonly IValidator<UpdateBookCommand> _updateBookCommandValidaor;
        private readonly IBookRepository _bookRepository;
        private readonly IEventPublisher<BookEvent> _eventPublisher;

        public UpdateBookCommandHandler(IValidator<UpdateBookCommand> bookCommandValidaor,
            IBookRepository bookRepository,
            IEventPublisher<BookEvent> eventPublisher)
        {
            _updateBookCommandValidaor = bookCommandValidaor;
            _bookRepository = bookRepository;
            _eventPublisher = eventPublisher;
        }

        public Result Handle(UpdateBookCommand command)
        {
            var validationResult = Validate(command, _updateBookCommandValidaor);

            if (validationResult.IsValid)
            {
                var book = BookMapper.CommandToEntity(command);
                _bookRepository.Update(book);
                _bookRepository.Commit();

                var newBook = _bookRepository.GetById(book.Id);
                _eventPublisher.Publish(BookMapper.EntityToEvent(newBook));
            }

            return Return();
        }
    }
}