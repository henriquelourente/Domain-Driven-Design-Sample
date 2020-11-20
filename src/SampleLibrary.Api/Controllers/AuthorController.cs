using Microsoft.AspNetCore.Mvc;
using SampleLibrary.Application.Author;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Author;

namespace SampleLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ICommandHandler<CreateAuthorCommand> _createAuthorCommandHandler;
        private readonly ICommandHandler<UpdateAuthorCommand> _updateAuthorCommandHandler;
        private readonly IAuthorQueries _authorQueries;

        public AuthorController(ICommandHandler<CreateAuthorCommand> createAuthorCommandHandler,
            ICommandHandler<UpdateAuthorCommand> updateAuthorCommandHandler,
            IAuthorQueries authorQueries)
        {
            _createAuthorCommandHandler = createAuthorCommandHandler;
            _updateAuthorCommandHandler = updateAuthorCommandHandler;
            _authorQueries = authorQueries;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_authorQueries.GetAllAsync().Result);
        }

        [HttpPost]
        public IActionResult Post(CreateAuthorCommand command)
        {
            var result =_createAuthorCommandHandler.Handle(command);

            if (result.Success)
                return Ok(command);

            return BadRequest(result.Errors);
        }

        [HttpPut]
        public IActionResult Put(UpdateAuthorCommand command)
        {
            var result = _updateAuthorCommandHandler.Handle(command);
            
            if (result.Success)
                return Ok(command);

            return BadRequest(result.Errors);
        }
    }
}
