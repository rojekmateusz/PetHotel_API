using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Animal.Command.CreateAnimal;

public class CreateAnimalCommandHandlerTests
{
    private readonly Mock<ILogger<CreateAnimalCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IAnimalRepository> _animalRepositoryMock;
    private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<IOwnerAuthorizationService> _ownerAuthorizationService;
    private readonly CreateAnimalCommandHandler _handler;

    public CreateAnimalCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateAnimalCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _animalRepositoryMock = new Mock<IAnimalRepository>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _ownerAuthorizationService = new Mock<IOwnerAuthorizationService>();

        _handler = new CreateAnimalCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _animalRepositoryMock.Object,
            _ownerRepositoryMock.Object,
            _ownerAuthorizationService.Object);
    }

    [Fact]
    public async Task Handle_ForValidCommand_ReturnsCreatedAnimalId()
    {
        // arrange
        var command = new CreateAnimalCommand
        {
            OwnerID = 1
        };
        
        var animal = new Domain.Entities.Animal();
        var owner = new Domain.Entities.Owner();

        _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(command.OwnerID))
            .ReturnsAsync(owner);

        _mapperMock.Setup(m => m.Map<Domain.Entities.Animal>(command))
            .Returns(animal);
            
        _animalRepositoryMock.Setup(repo => repo.CreateAnimal(It.IsAny<Domain.Entities.Animal>()))
            .ReturnsAsync(1);

        _ownerAuthorizationService.Setup(service => service.Authorize(owner, ResourceOperation.Create))
            .Returns(true);

        // act
        var result = await _handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().Be(1);
        _animalRepositoryMock.Verify(r => r.CreateAnimal(animal), Times.Once);
        _mapperMock.Verify(m => m.Map<Domain.Entities.Animal>(command), Times.Once);
        _ownerRepositoryMock.Verify(repo => repo.GetOwnerByIdAsync(command.OwnerID), Times.Once);
        _ownerAuthorizationService.Verify(service => service.Authorize(owner, ResourceOperation.Create), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingOwner_ShouldThrowNotFoundException()
    {
        // Arrange
        
        var request = new CreateAnimalCommand
        {
            OwnerID = 2
        };

        _ownerRepositoryMock.Setup(r => r.GetOwnerByIdAsync(request.OwnerID))
                .ReturnsAsync((Domain.Entities.Owner?)null);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Owner with id: {request.OwnerID} doesn't exist");
    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // / Arrange
        var ownerId = 3;
        var request = new CreateAnimalCommand
        {
            OwnerID = ownerId
        };

        var existingOwner = new Domain.Entities.Owner
        {
            Id = ownerId
        };

        _ownerRepositoryMock
            .Setup(r => r.GetOwnerByIdAsync(ownerId))
                .ReturnsAsync(existingOwner);

        _ownerAuthorizationService
            .Setup(a => a.Authorize(existingOwner, ResourceOperation.Create))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbidException>();
    }
}