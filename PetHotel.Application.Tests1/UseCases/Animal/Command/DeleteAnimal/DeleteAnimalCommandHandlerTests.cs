using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Animal.Command.DeleteAnimal;

public class DeleteAnimalCommandHandlerTests
{
    private readonly Mock<ILogger<DeleteAnimalCommandHandler>> _loggerMock;
    private readonly Mock<IAnimalRepository> _animalRepositoryMock;
    private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<IOwnerAuthorizationService> _ownerAuthorizationService;
    private readonly DeleteAnimalCommandHandler _handler;

    public DeleteAnimalCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<DeleteAnimalCommandHandler>>();
        _animalRepositoryMock = new Mock<IAnimalRepository>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _ownerAuthorizationService = new Mock<IOwnerAuthorizationService>();

        _handler = new DeleteAnimalCommandHandler(
            _loggerMock.Object,
            _animalRepositoryMock.Object,
            _ownerRepositoryMock.Object,
            _ownerAuthorizationService.Object);
    }
    [Fact()]
    public async Task Handle_ForValidCommand_DeletedAnimal()
    {
        // arrange

        var command = new DeleteAnimalCommand(1, 1)
        {
            Id = 1,
            OwnerId = 1
        };

        var animal = new Domain.Entities.Animal();
        var owner = new Domain.Entities.Owner();

        _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(command.OwnerId))
            .ReturnsAsync(owner);

        _animalRepositoryMock.Setup(repo => repo.GetAnimalByIdAsync(command.Id))
            .ReturnsAsync(animal);

        _animalRepositoryMock.Setup(repo => repo.DeleteAnimal(animal));

        _ownerAuthorizationService.Setup(service => service.Authorize(owner, Domain.Constants.ResourceOperation.Delete))
            .Returns(true);

        // act

        await _handler.Handle(command, CancellationToken.None);

        // assert

        _animalRepositoryMock.Verify(r => r.DeleteAnimal(animal), Times.Once);
        _ownerRepositoryMock.Verify(repo => repo.GetOwnerByIdAsync(command.OwnerId), Times.Once);
        _ownerAuthorizationService.Verify(service => service.Authorize(owner, ResourceOperation.Delete), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingOwner_ShouldThrowNotFoundException()
    {
        // Arrange

        var request = new DeleteAnimalCommand(1,1)
        {
            Id = 2,
            OwnerId = 2
        };

        _ownerRepositoryMock.Setup(r => r.GetOwnerByIdAsync(request.OwnerId))
                .ReturnsAsync((Domain.Entities.Owner?)null);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Owner with id: {request.OwnerId} doesn't exist");
    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // / Arrange
        var ownerId = 3;
        var request = new DeleteAnimalCommand(3,3)
        {   
            OwnerId = ownerId
        };

        var existingOwner = new Domain.Entities.Owner
        {
            Id = ownerId
        };

        _ownerRepositoryMock
            .Setup(r => r.GetOwnerByIdAsync(ownerId))
                .ReturnsAsync(existingOwner);

        _ownerAuthorizationService
            .Setup(a => a.Authorize(existingOwner, ResourceOperation.Delete))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbidException>();
    }

    [Fact]
    public async Task Handle_WithNonExistingAnimal_ShouldThrowNotFoundException()
    {
        // Arrange

        var request = new DeleteAnimalCommand(2, 2)
        {
            Id = 2,
            OwnerId = 2
        };

        var owner = new Domain.Entities.Owner
        {
            Id = request.OwnerId
        };

        _ownerRepositoryMock.Setup(r => r.GetOwnerByIdAsync(request.OwnerId))
                .ReturnsAsync(owner);

        _ownerAuthorizationService
                .Setup(a => a.Authorize(owner, ResourceOperation.Delete))
                .Returns(true);

        _animalRepositoryMock.Setup(r => r.GetAnimalByIdAsync(request.Id))
                .ReturnsAsync((Domain.Entities.Animal?)null);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Animal with id: {request.Id} doesn't exist");
    }
}