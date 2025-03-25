using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Hotel.Command.CreateHotel;
using PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;
using PetHotel.Application.UseCases.Hotel.Dto;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Hotel.Dto;

public class HotelProfileTests
{
    private IMapper _mapper;
    public HotelProfileTests()
    {
        var configure = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<HotelProfile>();
        });

        _mapper = configure.CreateMapper();
    }

    [Fact()]
    public void CreateMap_FromHotelToHotelDto_MapsCorrectly()
    {
        // arrange

        var hotel = new Domain.Entities.Hotel
        {
            Id = 1,
            HotelName = "Name Test",
            HotelsNIP = "Nip Test",
            HotelOwnerName = "OwnerName Test",
            Address = "Address Test",
            PhoneNumber = "PhoneNumber Test",
            City = "City Test",
            PostalCode = "Postal Test",
            Email = "Email Test",
            Description = "Description Test",
            IsActive = "Active Test",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        // act

        var hotelDto = _mapper.Map<HotelDto>(hotel);

        // assert

        hotelDto.Should().NotBeNull();
        hotelDto.Id.Should().Be(hotel.Id);
        hotelDto.HotelName.Should().Be(hotel.HotelName);
        hotelDto.HotelsNIP.Should().Be(hotel.HotelsNIP);
        hotelDto.HotelOwnerName.Should().Be(hotel.HotelOwnerName);
        hotelDto.Address.Should().Be(hotel.Address);
        hotelDto.PhoneNumber.Should().Be(hotel.PhoneNumber);
        hotelDto.City.Should().Be(hotel.City);
        hotelDto.PostalCode.Should().Be(hotel.PostalCode);
        hotelDto.Email.Should().Be(hotel.Email);
        hotelDto.Description.Should().Be(hotel.Description);
        hotelDto.AverageRating.Should().Be(hotel.AverageRating);
        hotelDto.IsActive.Should().Be(hotel.IsActive);
        hotelDto.CreatedAt.Should().Be(hotel.CreatedAt);
        hotelDto.UpdatedAt.Should().Be(hotel.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_UpdateHotelToHotel_MapsCorrectly()
    {
        // arrange

        var command = new UpdateHotelCommand
        {
            Id = 1,
            HotelName = "Name Test",
            HotelsNIP = "Nip Test",
            HotelOwnerName = "OwnerName Test",
            Address = "Address Test",
            PhoneNumber = "PhoneNumber Test",
            City = "City Test",
            PostalCode = "Postal Test",
            Email = "Email Test",
            Description = "Description Test",
            IsActive = "Active Test",
            UpdatedAt = DateTime.Now,
        };

        // act

        var hotel = _mapper.Map<Domain.Entities.Hotel>(command);

        // assert

        hotel.Should().NotBeNull();
        hotel.Id.Should().Be(hotel.Id);
        hotel.HotelName.Should().Be(hotel.HotelName);
        hotel.HotelsNIP.Should().Be(hotel.HotelsNIP);
        hotel.HotelOwnerName.Should().Be(hotel.HotelOwnerName);
        hotel.Address.Should().Be(hotel.Address);
        hotel.PhoneNumber.Should().Be(hotel.PhoneNumber);
        hotel.City.Should().Be(hotel.City);
        hotel.PostalCode.Should().Be(hotel.PostalCode);
        hotel.Email.Should().Be(hotel.Email);
        hotel.Description.Should().Be(hotel.Description);
        hotel.AverageRating.Should().Be(hotel.AverageRating);
        hotel.IsActive.Should().Be(hotel.IsActive);
        hotel.UpdatedAt.Should().Be(hotel.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_CreateHotelToHotel_MapsCorrectly()
    {
        // arrange

        var command = new CreateHotelCommand
        {
            HotelName = "Name Test",
            HotelsNIP = "Nip Test",
            HotelOwnerName = "OwnerName Test",
            Address = "Address Test",
            PhoneNumber = "PhoneNumber Test",
            City = "City Test",
            PostalCode = "Postal Test",
            Email = "Email Test",
            Description = "Description Test",
            IsActive = "Active Test",
            CreatedAt = DateTime.Now,
        };

        // act

        var hotel = _mapper.Map<Domain.Entities.Hotel>(command);

        // assert

        hotel.Should().NotBeNull();
        hotel.HotelName.Should().Be(hotel.HotelName);
        hotel.HotelsNIP.Should().Be(hotel.HotelsNIP);
        hotel.HotelOwnerName.Should().Be(hotel.HotelOwnerName);
        hotel.Address.Should().Be(hotel.Address);
        hotel.PhoneNumber.Should().Be(hotel.PhoneNumber);
        hotel.City.Should().Be(hotel.City);
        hotel.PostalCode.Should().Be(hotel.PostalCode);
        hotel.Email.Should().Be(hotel.Email);
        hotel.Description.Should().Be(hotel.Description);
        hotel.AverageRating.Should().Be(hotel.AverageRating);
        hotel.IsActive.Should().Be(hotel.IsActive);
        hotel.CreatedAt.Should().Be(hotel.CreatedAt);
    }
}