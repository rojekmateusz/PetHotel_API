using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.UseCases.Room.Command.CreateRoom.Tests
{
    public class CreateRoomCommandHandlerTests
    {
        private readonly Mock<ILogger<CreateRoomCommandHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly Mock<IHotelAuthorizationService> _hotelAuthorizationServiceMock;
        private readonly CreateRoomCommandHandler _handler;

        public CreateRoomCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<CreateRoomCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _hotelAuthorizationServiceMock = new Mock<IHotelAuthorizationService>();
            _handler = new CreateRoomCommandHandler(

                _loggerMock.Object,
                _mapperMock.Object,
                _roomRepositoryMock.Object,
                _hotelRepositoryMock.Object,
                _hotelAuthorizationServiceMock.Object
                );
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRoomId()
        {
            // arrange

            var command = new CreateRoomCommand();

            var hotel = new Domain.Entities.Hotel();
            var room = new Domain.Entities.Room();

            _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(command.HotelId))
                .ReturnsAsync(hotel);

            _mapperMock.Setup(m => m.Map<Domain.Entities.Room>(command))
                .Returns(room);

            _roomRepositoryMock.Setup(repo => repo.CreateRoom(It.IsAny<Domain.Entities.Room>()))
                .ReturnsAsync(1);

            _hotelAuthorizationServiceMock.Setup(service => service.Authorize(hotel, Domain.Constants.ResourceOperation.Create))
                .Returns(true);

            // act

            var result = await  _handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(1);
        }

        [Fact()]
        public async Task Handle_WithNonExistingHotel_ShouldThrowNotFoundException()
        {
            // arrange

            var command = new CreateRoomCommand {

                HotelId = 1

                };
            
            _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(command.HotelId))
                .ReturnsAsync((Domain.Entities.Hotel?)null);
            
            // act
            
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            
            // assert
            
            await act.Should().ThrowAsync<PetHotel.Domain.Exceptions.NotFoundException>();
        }

        [Fact]
        public async Task Handle_WithUnauthorizedHotel_ShouldThrowForbidException()
        {
            // arrange
            
            var command = new CreateRoomCommand
            {
                HotelId = 1
            };
            
            var hotel = new Domain.Entities.Hotel();
            
            _hotelRepositoryMock.Setup(repo => repo.GetHotelByIdAsync(command.HotelId))
                .ReturnsAsync(hotel);
            
            _hotelAuthorizationServiceMock.Setup(service => service.Authorize(hotel, Domain.Constants.ResourceOperation.Create))
                .Returns(false);
            
            // act
            
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            
            // assert
            
            await act.Should().ThrowAsync<PetHotel.Domain.Exceptions.ForbidException>();
        }
    }
}