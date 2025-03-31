using AutoMapper;
using Azure.Core;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Owner.Command.CreateOwner;
using PetHotel.Application.User;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Owner.Command.CreateOwner;

public class CreateOwnerCommandHandlerTests
{
    private readonly Mock<ILogger<CreateOwnerCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly CreateOwnerCommandHandler _handler;

    public CreateOwnerCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateOwnerCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _userContextMock = new Mock<IUserContext>();
        _handler = new CreateOwnerCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _ownerRepositoryMock.Object,
            _userContextMock.Object);
    }


    [Fact()]
    public async void Handle_ForValidCommand_ReturnCreatedOwnerId()
    {
        // arrange

        var command = new CreateOwnerCommand();

        var currentUser = new CurrentUser("1", "test@test.com", []);
        _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var owner = new Domain.Entities.Owner();

        _mapperMock.Setup(o => o.Map<Domain.Entities.Owner>(command))
            .Returns(owner);

        owner.UserId = currentUser.Id;

        _ownerRepositoryMock.Setup(repo => repo.CreateOwner(It.IsAny<Domain.Entities.Owner>()))
            .ReturnsAsync(1);

        // act

        var result = await _handler.Handle(command, CancellationToken.None);

        // assert

        result.Should().Be(1);
    }

    [Fact()]
    public async void Handle_WithExistingOwner_ShouldThrowOwnerAlreadyExists()
    {

        // arrange
        var request = new CreateOwnerCommand();

        var existingOwner = new Domain.Entities.Owner
        {
            UserId = "2"
        };
                    
        var currentUser = new CurrentUser("2", "test@test.com", []);
        _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
                      

        _ownerRepositoryMock.Setup(or => or.DoesOwnerExist(currentUser.Id))
                .ReturnsAsync(true);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<OwnerAlreadyExistsException>()
                .WithMessage($" User with Id: {currentUser.Id} already has an assigned owner");
    }
}