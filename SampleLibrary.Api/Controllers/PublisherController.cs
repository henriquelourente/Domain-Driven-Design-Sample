using Microsoft.AspNetCore.Mvc;
using SampleLibrary.Application.Publisher;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Publisher;

namespace SampleLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly ICommandHandler<CreatePublisherCommand> _createPublisherCommandHandler;
        private readonly ICommandHandler<UpdatePublisherCommand> _updatePublisherCommandHandler;
        private readonly IPublisherQueries _publisherQueries;

        public PublisherController(ICommandHandler<CreatePublisherCommand> createPublisherCommandHandler,
            ICommandHandler<UpdatePublisherCommand> updatePublisherCommandHandler,
            IPublisherQueries publisherQueries)
        {
            _createPublisherCommandHandler = createPublisherCommandHandler;
            _updatePublisherCommandHandler = updatePublisherCommandHandler;
            _publisherQueries = publisherQueries;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_publisherQueries.GetAll().Result);
        }

        [HttpPost]
        public IActionResult Post(CreatePublisherCommand command)
        {
            var result = _createPublisherCommandHandler.Handle(command);
            if (result.Success)
                return Ok(command);

            return BadRequest(result.Errors);
        }

        [HttpPut]
        public IActionResult Put(UpdatePublisherCommand command)
        {
            var result = _updatePublisherCommandHandler.Handle(command);
            if (result.Success)
                return Ok(command);

            return BadRequest(result.Errors);
        }
    }
}