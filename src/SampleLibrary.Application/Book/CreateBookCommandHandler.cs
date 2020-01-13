using FluentValidation;
using SampleLibrary.Application.AutoMapper;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Book
{
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand>
    {
        private readonly IValidator<CreateBookCommand> _createBookCommandValidaor;
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IValidator<CreateBookCommand> bookCommandValidaor,
            IBookRepository bookRepository)
        {
            _createBookCommandValidaor = bookCommandValidaor;
            _bookRepository = bookRepository;
        }

        public void Handle(CreateBookCommand command)
        {
            var validationResult = _createBookCommandValidaor.Validate(command);

            if (!validationResult.IsValid)
                return;

            var book = BookMapper.CommandToEntity(command);
            _bookRepository.Add(book);
            _bookRepository.Commit();
        }
    }
}