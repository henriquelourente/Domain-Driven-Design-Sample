using Microsoft.AspNetCore.Mvc;
using SampleLibrary.Application.Book;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Book;

namespace SampleLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ICommandHandler<CreateBookCommand> _createBookCommandHandler;
        private readonly ICommandHandler<UpdateBookCommand> _updateBookCommandHandler;
        private readonly IBookQueries _bookQueries;

        public BookController(ICommandHandler<CreateBookCommand> createBookCommandHandler,
            ICommandHandler<UpdateBookCommand> updateBookCommandHandler,
            IBookQueries bookQueries)
        {
            _createBookCommandHandler = createBookCommandHandler;
            _updateBookCommandHandler = updateBookCommandHandler;
            _bookQueries = bookQueries;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookQueries.GetAll().Result);
        }

        [HttpPost]
        public IActionResult Post(CreateBookCommand command)
        {
            _createBookCommandHandler.Handle(command);
            return Ok(command);
        }

        [HttpPut]
        public IActionResult Put(UpdateBookCommand command)
        {
            _updateBookCommandHandler.Handle(command);
            return Ok(command);
        }
    }
}