using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Hotel.Command.CreateHotel;
using PetHotel.Application.User;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Hotel.Command.CreateHotel;

public class CreateHotelCommandHandlerTests
{
    private readonly Mock<ILogger<CreateHotelCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly CreateHotelCommandHandler _handler;

    public CreateHotelCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateHotelCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _userContextMock = new Mock<IUserContext>();
        _handler = new CreateHotelCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _hotelRepositoryMock.Object,
            _userContextMock.Object);
    }

    [Fact()]
    public async void Handle_ForValidCommand_ReturnsCreatedHotelId()
    {
        // arrange

        var command = new CreateHotelCommand();

        var currentUser = new CurrentUser("1", "test@test.com", []);
        _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var hotel = new Domain.Entities.Hotel();
        _mapperMock.Setup(m => m.Map<Domain.Entities.Hotel>(command))
            .Returns(hotel);

        hotel.UserId = currentUser.Id;
        _hotelRepositoryMock.Setup(repo => repo.CreateHotel(It.IsAny<Domain.Entities.Hotel>()))
            .ReturnsAsync(1);

        // act

        var result = await _handler.Handle(command, CancellationToken.None);

        // assert

        result.Should().Be(1);
    }    
}