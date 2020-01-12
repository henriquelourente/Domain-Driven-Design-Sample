using System;
using FluentValidation;
using Moq;
using SampleLibrary.Application.Publisher;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Commands.Publisher.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using Xunit;

namespace SampleLibrary.Application.Tests.Publisher.CommandHandlers
{
    public class CreatePublisherCommandHandlerTests
    {
        private readonly CreatePublisherCommandHandler _createPublisherCommandHandler;
        private readonly Mock<IPublisherRepository> _publisherRepository;

        public CreatePublisherCommandHandlerTests()
        {
            _publisherRepository = new Mock<IPublisherRepository>();
            IValidator<CreatePublisherCommand> createPublisherCommandValidator =
                new CreatePublisherCommandValidator(_publisherRepository.Object);

            _createPublisherCommandHandler =
                new CreatePublisherCommandHandler(createPublisherCommandValidator, _publisherRepository.Object);
        }

        [Fact]
        public void Should_Create_When_Command_Is_Valid()
        {
            //Arrange
            var publisher = new CreatePublisherCommand {Name = "Robert Cecil Martin"};

            //Act
            _createPublisherCommandHandler.Handle(publisher);

            //Assert
            _publisherRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Publisher>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Create_When_Command_Is_Invalid()
        {
            //Arrange
            var publisher = new CreatePublisherCommand {Name = String.Empty};

            //Act
            _createPublisherCommandHandler.Handle(publisher);

            //Assert
            _publisherRepository.Verify(r => r.Add(It.IsAny<Domain.Entities.Publisher>()), Times.Never);
        }
    }
}