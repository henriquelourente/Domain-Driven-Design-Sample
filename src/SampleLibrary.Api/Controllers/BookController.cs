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
        private readonly ICommandHandler<DeleteBookCommand> _deleteBookCommandHandler;
        private readonly IBookQueries _bookQueries;

        public BookController(ICommandHandler<CreateBookCommand> createBookCommandHandler,
            ICommandHandler<UpdateBookCommand> updateBookCommandHandler,
            ICommandHandler<DeleteBookCommand> deleteBookCommandHandler,
            IBookQueries bookQueries)
        {
            _createBookCommandHandler = createBookCommandHandler;
            _updateBookCommandHandler = updateBookCommandHandler;
            _deleteBookCommandHandler = deleteBookCommandHandler;
            _bookQueries = bookQueries;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookQueries.GetAllAsync().Result);
        }

        [HttpGet]
        [Route("{text}")]
        public IActionResult Get(string text)
        {
            return Ok(_bookQueries.GetByTextAsync(text).Result);
        }

        [HttpPost]
        public IActionResult Post(CreateBookCommand command)
        {
            var result = _createBookCommandHandler.Handle(command);

            if (result.Success)
                return Ok(command);

            return BadRequest(result.Errors);
        }

        [HttpPut]
        public IActionResult Put(UpdateBookCommand command)
        {
            var result = _updateBookCommandHandler.Handle(command);

            if (result.Success)
                return Ok(command);

            return BadRequest(result.Errors);
        }

        [HttpDelete]
        public IActionResult Delete(DeleteBookCommand command)
        {
            var result = _deleteBookCommandHandler.Handle(command);

            if (result.Success)
                return Ok(command);

            return BadRequest(result.Errors);
        }
    }
}