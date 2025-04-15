
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;
using PetHotel.Application.UseCases.Owner.Command.UpdateOwner;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Owner.Command.UpdateOwner;

public class UpdateOwnerCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateOwnerCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<IOwnerAuthorizationService> _ownerAuthorizationServiceMock;
    private readonly UpdateOwnerCommandHandler _handler;

    public UpdateOwnerCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateOwnerCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _ownerAuthorizationServiceMock = new Mock<IOwnerAuthorizationService>();
        _handler = new UpdateOwnerCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _ownerRepositoryMock.Object,
            _ownerAuthorizationServiceMock.Object
        );
    }

    [Fact()]
    public async void Handle_ForValidCommand_ReturnsUpdatedOwner()
    {
        // arrange

        var command = new UpdateOwnerCommand
        {
            Id = 1
        };

        var owner = new Domain.Entities.Owner
        {
            Id = 1
        };

        _ownerRepositoryMock.Setup(x => x.GetOwnerByIdAsync(command.Id))
            .ReturnsAsync(owner);
        _ownerAuthorizationServiceMock.Setup(x => x.Authorize(owner, ResourceOperation.Update))
            .Returns(true);
        _mapperMock.Setup(x => x.Map(command, owner))
            .Returns(owner);

        // act

        await _handler.Handle(command, CancellationToken.None);

        // assert

        _ownerRepositoryMock.Verify(x => x.GetOwnerByIdAsync(command.Id), Times.Once);
        _ownerAuthorizationServiceMock.Verify(x => x.Authorize(owner, ResourceOperation.Update), Times.Once);
        _mapperMock.Verify(x => x.Map(command, owner), Times.Once);

    }

    [Fact]
    public async Task Handle_WithNonExistingOwner_ShouldThrownNotFoundException()
    {
        // arrange

        var request = new UpdateOwnerCommand
        {
            Id = 1,

        };

        _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(request.Id))
            .ReturnsAsync((Domain.Entities.Owner?)null);

        // act 

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Owner with id: {request.Id} doesn't exist");
    }


    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // arrange

        var request = new UpdateOwnerCommand
        {
            Id = 2,

        };

        var existingOwner = new Domain.Entities.Owner
        {
            Id = 3
        };

        _ownerRepositoryMock.
            Setup(repo => repo.GetOwnerByIdAsync(request.Id))
                .ReturnsAsync(new Domain.Entities.Owner());

        _ownerAuthorizationServiceMock
            .Setup(a => a.Authorize(existingOwner, ResourceOperation.Update))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<ForbidException>();
    }
}