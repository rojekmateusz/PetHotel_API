using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Animal.Command.UpdateAnimal;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Animal.Command.UpdateAnimal;

public class UpdateAniamlCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateAniamlCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IAnimalRepository> _animalRepositoryMock;
    private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<IOwnerAuthorizationService> _ownerAuthorizationService;
    private readonly UpdateAniamlCommandHandler _handler;

    public UpdateAniamlCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateAniamlCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _animalRepositoryMock = new Mock<IAnimalRepository>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _ownerAuthorizationService = new Mock<IOwnerAuthorizationService>();

        _handler = new UpdateAniamlCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _animalRepositoryMock.Object,
            _ownerRepositoryMock.Object,
            _ownerAuthorizationService.Object);
    }

    [Fact()]
    public void Handle_ForValidCommand_ReturnsUpdatedAnimal()
    {
        // arrange
    }
}