using FluentValidation;
using SampleLibrary.Application.Mapping;
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
        private readonly AppForger _forger;

        public UpdateBookCommandHandler(IValidator<UpdateBookCommand> bookCommandValidaor,
            IBookRepository bookRepository,
            IEventPublisher<BookEvent> eventPublisher,
            AppForger forger)
        {
            _updateBookCommandValidaor = bookCommandValidaor;
            _bookRepository = bookRepository;
            _eventPublisher = eventPublisher;
            _forger = forger;
        }

        public Result Handle(UpdateBookCommand command)
        {
            var validationResult = Validate(command, _updateBookCommandValidaor);

            if (validationResult.IsValid)
            {
                var book = _forger.Forge(command);
                _bookRepository.Update(book);
                _bookRepository.SaveChanges();

                var newBook = _bookRepository.GetById(book.Id);
                _eventPublisher.Publish(_forger.Forge(newBook));
            }

            return Return();
        }
    }
}
