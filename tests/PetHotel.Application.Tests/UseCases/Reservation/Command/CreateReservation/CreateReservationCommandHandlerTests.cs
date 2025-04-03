using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Payment.Command.CreatePayment;
using PetHotel.Application.UseCases.Reservation.Command.CreateReservation;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Reservation.Command.CreateReservation;

public class CreateReservationCommandHandlerTests
{
    private readonly Mock<ILogger<CreateReservationCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<IReservationRepository> _reservationRepositoryMock;
    private readonly Mock<IOwnerAuthorizationService> _ownerAuthorizationServiceMock;
    private readonly CreateReservationCommandHandler _handler;

    public CreateReservationCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateReservationCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _reservationRepositoryMock = new Mock<IReservationRepository>();
        _ownerAuthorizationServiceMock = new Mock<IOwnerAuthorizationService>();
        _handler = new CreateReservationCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _hotelRepositoryMock.Object,
            _ownerRepositoryMock.Object,
            _reservationRepositoryMock.Object,
            _ownerAuthorizationServiceMock.Object
            );
    }

    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedReservationId()
    {
        // arrange

        var command = new CreateReservationCommand();

        var hotel = new Domain.Entities.Hotel();
        var owner = new Domain.Entities.Owner();
        var reservation = new Domain.Entities.Reservation();
        var service = new Domain.Entities.Service();

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(command.HotelId))
            .ReturnsAsync(hotel);
        _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(command.OwnerId))
            .ReturnsAsync(owner);
        _mapperMock.Setup(m => m.Map<Domain.Entities.Reservation>(command))
            .Returns(reservation);
        _reservationRepositoryMock.Setup(repo => repo.CreateReservation(It.IsAny<Domain.Entities.Reservation>()))
            .ReturnsAsync(1);
        _ownerAuthorizationServiceMock.Setup(service => service.Authorize(owner, ResourceOperation.Create))
            .Returns(true);

        // act

        var result = await _handler.Handle(command, CancellationToken.None);

        //assert

        result.Should().Be(1);
    }

    [Fact]
    public async Task Handle_WithNonExistingHotel_ShouldThrownNotFoundException()
    {
        // arrange

        var request = new CreateReservationCommand
        {
            HotelId = 1
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
    public async Task Handle_WithNonExistingOwner_ShouldThrownNotFoundException()
    {
        // arrange

        var request = new CreateReservationCommand
        {
            HotelId = 2,
            OwnerId = 1            
        };

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(request.HotelId))
            .ReturnsAsync(new Domain.Entities.Hotel());

        _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(request.OwnerId))
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
        var request = new CreateReservationCommand
        {
            HotelId = 2,
            OwnerId = ownerId
        };

        var existingOwner = new Domain.Entities.Owner
        {
            Id = ownerId
        };

        _hotelRepositoryMock.
            Setup(repo => repo.GetHotelByIdAsync(request.HotelId))
                .ReturnsAsync(new Domain.Entities.Hotel());

        _ownerRepositoryMock
            .Setup(r => r.GetOwnerByIdAsync(ownerId))
                .ReturnsAsync(existingOwner);

        _ownerAuthorizationServiceMock
            .Setup(a => a.Authorize(existingOwner, ResourceOperation.Create))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<ForbidException>();
    }
}