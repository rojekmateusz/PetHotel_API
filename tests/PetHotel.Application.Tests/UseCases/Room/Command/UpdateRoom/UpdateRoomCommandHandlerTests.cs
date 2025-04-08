using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Reservation.Command.CreateReservation;
using PetHotel.Application.UseCases.Room.Command.UpdateRoom;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PetHotel.Application.Tests.UseCases.Room.Command.UpdateRoom;

public class UpdateRoomCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRoomCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IHotelAuthorizationService> _hotelAuthorizationServiceMock;
    private readonly UpdateRoomCommandHandler _handler;

    public UpdateRoomCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRoomCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _hotelAuthorizationServiceMock = new Mock<IHotelAuthorizationService>();
        _handler = new UpdateRoomCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _hotelRepositoryMock.Object,
            _roomRepositoryMock.Object,
            _hotelAuthorizationServiceMock.Object);
    }

    [Fact()]
    public async Task Handler_WithValidCommand_ShouldThrowNoContent()
    {
        var command = new UpdateRoomCommand
        {
            Id = 1,
            HotelId = 1
        };

        var existingRoom = new Domain.Entities.Room
        {
            Id = 1,
            HotelId = 1
        };

        var hotel = new Domain.Entities.Hotel
        {
            Id = 1,
            Rooms = new List<Domain.Entities.Room> { existingRoom }
        };

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(command.HotelId))
            .ReturnsAsync(hotel);

        _mapperMock.Setup(m => m.Map(command, existingRoom))
            .Returns(existingRoom);

        _hotelAuthorizationServiceMock.Setup(service => service.Authorize(hotel, ResourceOperation.Update))
            .Returns(true);

        _roomRepositoryMock.Setup(repo => repo.SaveChanges())
            .Returns(Task.CompletedTask);

        // act 

        await _handler.Handle(command, CancellationToken.None);

        // assert

        _hotelRepositoryMock.Verify(repo => repo.GetHotelByIdAsync(command.HotelId), Times.Once);
        _roomRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once);
        _hotelAuthorizationServiceMock.Verify(service => service.Authorize(hotel, ResourceOperation.Update), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingHotel_ShouldThrownNotFoundException()
    {
        // arrange

        var request = new UpdateRoomCommand
        {
            HotelId = 1,
            
        };

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(request.HotelId))
            .ReturnsAsync((Domain.Entities.Hotel?)null);

        // act 

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Hotel with id: {request.HotelId} doesn't exist");
    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // / Arrange
        
        var request = new UpdateRoomCommand
        {
            HotelId = 2,
            
        };

        var existingHotel = new Domain.Entities.Hotel
        {
            Id = 2
        };

        _hotelRepositoryMock.
            Setup(repo => repo.GetHotelByIdAsync(request.HotelId))
                .ReturnsAsync(new Domain.Entities.Hotel());
                
        _hotelAuthorizationServiceMock
            .Setup(a => a.Authorize(existingHotel, ResourceOperation.Update))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<ForbidException>();
    }

    [Fact]
    public async Task Handle_WithNonExistingRoom_ShouldThrownNotFoundException()
    {
        // arrange

        
        var request = new UpdateRoomCommand
        {
            HotelId = 1,
            Id = 1
        };
                

        var hotel = new Domain.Entities.Hotel
        {
            Id = 1,
            Rooms = new List<Domain.Entities.Room> { }
        };

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(request.HotelId))
            .ReturnsAsync(hotel);

        _hotelAuthorizationServiceMock
            .Setup(a => a.Authorize(hotel, ResourceOperation.Update))
                .Returns(true);
                
        // act 

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Room with id: {request.Id} doesn't exist");
    }

}