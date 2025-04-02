
using AutoMapper;
using Azure.Storage.Blobs.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Room.Queries.GetAllRoomsByHotelId;
using PetHotel.Application.UseCases.Service.Command.CreateService;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Service.Command.CreateService;

public class CreateServiceCommandHandlerTests
{
    private readonly Mock<ILogger<CreateServiceCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IServiceRepository> _serviceRepositoryMock;
    private readonly Mock<IHotelAuthorizationService> _hotelAuthorizationServiceMock;
    private readonly CreateServiceCommandHandler _handler;

    public CreateServiceCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateServiceCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _serviceRepositoryMock = new Mock<IServiceRepository>();
        _hotelAuthorizationServiceMock = new Mock<IHotelAuthorizationService>();

        _handler = new CreateServiceCommandHandler(

            _loggerMock.Object,
            _mapperMock.Object,
            _hotelRepositoryMock.Object,
            _serviceRepositoryMock.Object,
            _hotelAuthorizationServiceMock.Object
             );
    }

    [Fact()]
    public async Task Handle_ForValidCommand_ReturnCreateServiceId()
    {
        // arrange

        var command = new CreateServiceCommand();

        var hotel = new Domain.Entities.Hotel();
        var service = new Domain.Entities.Service();

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(command.HotelId))
            .ReturnsAsync(hotel);

        _mapperMock.Setup(mapper => mapper.Map<Domain.Entities.Service>(command))
            .Returns(service);

        _serviceRepositoryMock.Setup(repo => repo.CreateService(It.IsAny<Domain.Entities.Service>()))
            .ReturnsAsync(1);

        _hotelAuthorizationServiceMock.Setup(service => service.Authorize(hotel, ResourceOperation.Create))
            .Returns(true);

        // act

        var result = await _handler.Handle(command, CancellationToken.None);

        // assert

        result.Should().Be(1);
    }

    [Fact()]
    public async Task Handle_WithNonExistingHotel_ShouldThrownNotFoundException()
    {
        // arrange 

        var request = new CreateServiceCommand
        {
            HotelId = 2
        };

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(request.HotelId))
            .ReturnsAsync((Domain.Entities.Hotel?)null);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Hotel with id: {request.HotelId} doesn't exist");
    }

    [Fact()]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // arrange

        var hotelId = 2;

        var request = new CreateServiceCommand
        {
            HotelId = hotelId
        };

        var existingHotel = new Domain.Entities.Hotel
        {
            Id = hotelId
        };

        _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(hotelId))
            .ReturnsAsync(existingHotel);

        _hotelAuthorizationServiceMock.Setup(service => service.Authorize(existingHotel, ResourceOperation.Create))
            .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<ForbidException>();
    }
}