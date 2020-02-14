using FluentValidation;
using SampleLibrary.Application.AutoMapper;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Domain.Events;

namespace SampleLibrary.Application.Book
{
    public class CreateBookCommandHandler : CommandHandlerBase, ICommandHandler<CreateBookCommand>
    {
        private readonly IValidator<CreateBookCommand> _createBookCommandValidaor;
        private readonly IBookRepository _bookRepository;
        private readonly IEventPublisher<BookEvent> _eventPublisher;

        public CreateBookCommandHandler(IValidator<CreateBookCommand> bookCommandValidaor,
            IBookRepository bookRepository,
            IEventPublisher<BookEvent> eventPublisher)
        {
            _createBookCommandValidaor = bookCommandValidaor;
            _bookRepository = bookRepository;
            _eventPublisher = eventPublisher;
        }

        public Result Handle(CreateBookCommand command)
        {
            var validationResult = Validate(command, _createBookCommandValidaor);

            if (validationResult.IsValid)
            {
                var book = BookMapper.CommandToEntity(command);
                _bookRepository.Add(book);
                _bookRepository.Commit();

                var newBook = _bookRepository.GetById(book.Id);
                _eventPublisher.Publish(BookMapper.EntityToEvent(newBook));
            }

            return Return();
        }
    }
}