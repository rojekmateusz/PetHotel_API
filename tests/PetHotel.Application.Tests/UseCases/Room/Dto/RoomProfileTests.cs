using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Room.Command.CreateRoom;
using PetHotel.Application.UseCases.Room.Command.UpdateRoom;
using PetHotel.Application.UseCases.Room.Dto;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Room.Dto;

public class RoomProfileTests
{
    private IMapper _mapper;
    public RoomProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RoomProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_FromRoomToRoomDto_MapsCorrectly()
    {
        // arrange

        var room = new Domain.Entities.Room
        {
            Id = 1,
            Capacity = 2,
            PricePerNight = 100,
            IsAvailable = "Yes",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // act

        var result = _mapper.Map<RoomDto>(room);

        // assert

        result.Should().NotBeNull();
        result.Id.Should().Be(room.Id);
        result.Capacity.Should().Be(room.Capacity);
        result.PricePerNight.Should().Be(room.PricePerNight);
        result.IsAvailable.Should().Be(room.IsAvailable);
        result.CreatedAt.Should().Be(room.CreatedAt);
        result.UpdatedAt.Should().Be(room.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_FromUpdateRoomToRoomDto_MapsCorrectly()
    {
        // arrange

        var room = new UpdateRoomCommand
        {
            Id = 1,
            Capacity = 2,
            PricePerNight = 100,
            IsAvailable = "Yes",
            UpdatedAt = DateTime.Now
        };

        // act

        var result = _mapper.Map<Domain.Entities.Room>(room);

        // assert

        result.Should().NotBeNull();
        result.Id.Should().Be(room.Id);
        result.Capacity.Should().Be(room.Capacity);
        result.PricePerNight.Should().Be(room.PricePerNight);
        result.IsAvailable.Should().Be(room.IsAvailable);
        result.UpdatedAt.Should().Be(room.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_FromCreateRoomToRoomDto_MapsCorrectly()
    {
        // arrange

        var room = new CreateRoomCommand
        {
            Capacity = 2,
            PricePerNight = 100,
            IsAvailable = "Yes",
            CreatedAt = DateTime.Now
        };

        // act

        var result = _mapper.Map<Domain.Entities.Room>(room);

        // assert

        result.Should().NotBeNull();
        result.Capacity.Should().Be(room.Capacity);
        result.PricePerNight.Should().Be(room.PricePerNight);
        result.IsAvailable.Should().Be(room.IsAvailable);
        result.CreatedAt.Should().Be(room.CreatedAt);
    }
}