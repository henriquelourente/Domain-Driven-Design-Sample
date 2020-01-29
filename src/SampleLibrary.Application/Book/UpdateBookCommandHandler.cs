using FluentValidation;
using SampleLibrary.Application.AutoMapper;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Book
{
    public class UpdateBookCommandHandler : CommandHandlerBase, ICommandHandler<UpdateBookCommand>
    {
        private readonly IValidator<UpdateBookCommand> _updateBookCommandValidaor;
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IValidator<UpdateBookCommand> bookCommandValidaor,
            IBookRepository bookRepository)
        {
            _updateBookCommandValidaor = bookCommandValidaor;
            _bookRepository = bookRepository;
        }

        public Result Handle(UpdateBookCommand command)
        {
            var validationResult = Validate(command, _updateBookCommandValidaor);

            if (validationResult.IsValid)
            {
                var book = BookMapper.CommandToEntity(command);
                _bookRepository.Update(book);
                _bookRepository.Commit();
            }
            return Return();
        }
    }
}