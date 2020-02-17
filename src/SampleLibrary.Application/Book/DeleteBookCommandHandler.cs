using FluentValidation;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Book.Validators;
using SampleLibrary.Domain.Events;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Book
{
    public class DeleteBookCommandHandler : CommandHandlerBase, ICommandHandler<DeleteBookCommand>
    {
        private readonly IValidator<DeleteBookCommand> _deleteBookCommandValidator;
        private readonly IBookRepository _bookRepository;
        private readonly IEventPublisher<DeleteBookEvent> _eventPublisher;

        public DeleteBookCommandHandler(
            IValidator<DeleteBookCommand> deleteBookCommandValidator, 
            IBookRepository bookRepository, 
            IEventPublisher<DeleteBookEvent> eventPublisher)
        {
            _deleteBookCommandValidator = deleteBookCommandValidator;
            _bookRepository = bookRepository;
            _eventPublisher = eventPublisher;
        }

        public Result Handle(DeleteBookCommand command)
        {
            var validationResult = Validate(command, _deleteBookCommandValidator);

            if (validationResult.IsValid)
            {
                _bookRepository.Delete(command.Id);
                _bookRepository.SaveChanges();

                _eventPublisher.Publish(new DeleteBookEvent { Id = command.Id});
            }

            return Return();
        }
    }
}