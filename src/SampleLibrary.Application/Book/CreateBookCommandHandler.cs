using FluentValidation;
using SampleLibrary.Application.AutoMapper;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Book
{
    public class CreateBookCommandHandler : CommandHandlerBase, ICommandHandler<CreateBookCommand>
    {
        private readonly IValidator<CreateBookCommand> _createBookCommandValidaor;
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IValidator<CreateBookCommand> bookCommandValidaor,
            IBookRepository bookRepository)
        {
            _createBookCommandValidaor = bookCommandValidaor;
            _bookRepository = bookRepository;
        }

        public Result Handle(CreateBookCommand command)
        {
            var validationResult = Validate(command, _createBookCommandValidaor);

            if (validationResult.IsValid)
            {
                var book = BookMapper.CommandToEntity(command);
                _bookRepository.Add(book);
                _bookRepository.Commit();
            }

            return Return();
        }
    }
}