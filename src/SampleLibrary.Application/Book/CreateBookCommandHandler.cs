using FluentValidation;
using SampleLibrary.Application.Mapping;
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
        private readonly AppForger _forger;

        public CreateBookCommandHandler(IValidator<CreateBookCommand> bookCommandValidaor,
            IBookRepository bookRepository,
            IEventPublisher<BookEvent> eventPublisher,
            AppForger forger)
        {
            _createBookCommandValidaor = bookCommandValidaor;
            _bookRepository = bookRepository;
            _eventPublisher = eventPublisher;
            _forger = forger;
        }

        public Result Handle(CreateBookCommand command)
        {
            var validationResult = Validate(command, _createBookCommandValidaor);

            if (validationResult.IsValid)
            {
                var book = _forger.Forge(command);
                _bookRepository.Add(book);
                _bookRepository.SaveChanges();

                var newBook = _bookRepository.GetById(book.Id);
                _eventPublisher.Publish(_forger.Forge(newBook));
            }

            return Return();
        }
    }
}
