using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;
using PetHotel.Application.UseCases.Room.Command.UpdateRoom;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Hotel.Command.UpdateHotel;

public class UpdateHotelCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateHotelCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IHotelAuthorizationService> _hotelAuthorizationServiceMock;
    private readonly UpdateHotelCommandHandler _handler;

    public UpdateHotelCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateHotelCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _hotelAuthorizationServiceMock = new Mock<IHotelAuthorizationService>();
        _handler = new UpdateHotelCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _hotelRepositoryMock.Object,
            _hotelAuthorizationServiceMock.Object);
    }

    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsUpdatedHotel()
    {
        // arrange

        var command = new UpdateHotelCommand
        {
            Id = 1
        };

        var hotel = new Domain.Entities.Hotel
        {
            Id = 1
        };

        _hotelRepositoryMock.Setup(x => x.GetHotelByIdAsync(command.Id))
            .ReturnsAsync(hotel);

        _mapperMock.Setup(x => x.Map(command, hotel))
            .Returns(hotel);
        
        _hotelRepositoryMock.Setup(x => x.SaveChanges())
            .Returns(Task.CompletedTask);

        _hotelAuthorizationServiceMock.Setup(x => x.Authorize(hotel, ResourceOperation.Update))
            .Returns(true);

        // act

        await _handler.Handle(command, CancellationToken.None);

        // assert

        _hotelRepositoryMock.Verify(x => x.GetHotelByIdAsync(command.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingHotel_ShouldThrownNotFoundException()
    {
        // arrange

        var request = new UpdateHotelCommand
        {
            Id = 1,

        };

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(request.Id))
            .ReturnsAsync((Domain.Entities.Hotel?)null);

        // act 

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Hotel with id: {request.Id} doesn't exist");
    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // arrange

        var request = new UpdateHotelCommand
        {
            Id = 2,

        };

        var existingHotel = new Domain.Entities.Hotel
        {
            Id = 3
        };

        _hotelRepositoryMock.
            Setup(repo => repo.GetHotelByIdAsync(request.Id))
                .ReturnsAsync(new Domain.Entities.Hotel());

        _hotelAuthorizationServiceMock
            .Setup(a => a.Authorize(existingHotel, ResourceOperation.Update))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<ForbidException>();
    }
}