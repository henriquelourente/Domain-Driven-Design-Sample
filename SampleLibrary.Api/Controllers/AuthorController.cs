using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleLibrary.Application.Author;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects;

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
            return Ok(_authorQueries.GetAll().Result);
        }

        [HttpPost]
        public IActionResult Post(CreateAuthorCommand command)
        {
            _createAuthorCommandHandler.Handle(command);
            return Ok(command);
        }

        [HttpPut]
        public IActionResult Put(UpdateAuthorCommand command)
        {
            _updateAuthorCommandHandler.Handle(command);
            return Ok(command);
        }
    }
}
