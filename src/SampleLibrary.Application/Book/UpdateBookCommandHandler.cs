using FluentValidation;
using SampleLibrary.Application.AutoMapper;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Interfaces.Repositories;

namespace SampleLibrary.Application.Book
{
    public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand>
    {
        private readonly IValidator<UpdateBookCommand> _updateBookCommandValidaor;
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IValidator<UpdateBookCommand> bookCommandValidaor,
            IBookRepository bookRepository)
        {
            _updateBookCommandValidaor = bookCommandValidaor;
            _bookRepository = bookRepository;
        }

        public void Handle(UpdateBookCommand command)
        {
            var validationResult = _updateBookCommandValidaor.Validate(command);

            if (!validationResult.IsValid)
                return;

            var book = BookMapper.CommandToEntity(command);
            _bookRepository.Update(book);
            _bookRepository.Commit();
        }
    }
}