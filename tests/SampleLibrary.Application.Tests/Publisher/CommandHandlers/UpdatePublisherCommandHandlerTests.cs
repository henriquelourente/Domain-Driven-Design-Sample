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
    public class UpdatePublisherCommandHandlerTests
    {
        private readonly UpdatePublisherCommandHandler _updatePublisherCommandHandler;
        private readonly Mock<IPublisherRepository> _publisherRepository;

        public UpdatePublisherCommandHandlerTests()
        {
            _publisherRepository = new Mock<IPublisherRepository>();
            IValidator<UpdatePublisherCommand> updatePublisherCommandValidator =
                new UpdatePublisherCommandValidator(_publisherRepository.Object);

            _updatePublisherCommandHandler =
                new UpdatePublisherCommandHandler(updatePublisherCommandValidator, _publisherRepository.Object);
        }

        [Fact]
        public void Should_Update_When_Command_Is_Valid()
        {
            //Arrange
            var publisher = new UpdatePublisherCommand { Id = Guid.NewGuid(), Name = "Pearson" };

            //Act
            _updatePublisherCommandHandler.Handle(publisher);

            //Assert
            _publisherRepository.Verify(r => r.Update(It.IsAny<Domain.Entities.Publisher>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Update_When_Command_Is_Invalid()
        {
            //Arrange
            var publisher = new UpdatePublisherCommand { Name = "Pearson" };

            //Act
            _updatePublisherCommandHandler.Handle(publisher);

            //Assert
            _publisherRepository.Verify(r => r.Update(It.IsAny<Domain.Entities.Publisher>()), Times.Never);
        }
    }
}