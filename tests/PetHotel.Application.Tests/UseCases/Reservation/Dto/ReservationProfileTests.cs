using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Reservation.Command.CreateReservation;
using PetHotel.Application.UseCases.Reservation.Dto;
using PetHotel.Application.UseCases.Review.Command.UpdateReview;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Reservation.Dto;

public class ReservationProfileTests
{
    private IMapper _mapper;
    public ReservationProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ReservationProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_FromReservationToReservationDto_MapsCorrectly()
    {
        // arrange

        var reservation = new Domain.Entities.Reservation
        {
            ReservationId = 1,
            StarDate = DateTime.Now,
            EndDate = DateTime.Now,
            Status = "Status Test",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            HotelId = 1,
            Notes = "Notes Test",
            AnimalId = 1,
            OwnerId = 1,
        };

        // act

        var reservationDto = _mapper.Map<ReservationDto>(reservation);

        // assert

        reservationDto.Should().NotBeNull();
        reservationDto.ReservationId.Should().Be(reservation.ReservationId);
        reservationDto.StarDate.Should().Be(reservation.StarDate);
        reservationDto.EndDate.Should().Be(reservation.EndDate);
        reservationDto.Status.Should().Be(reservation.Status);
        reservationDto.CreatedAt.Should().Be(reservation.CreatedAt);
        reservationDto.UpdatedAt.Should().Be(reservation.UpdatedAt);
        reservationDto.HotelId.Should().Be(reservation.HotelId);
        reservationDto.Notes.Should().Be(reservation.Notes);
        reservationDto.AnimalId.Should().Be(reservation.AnimalId);
        reservationDto.OwnerId.Should().Be(reservation.OwnerId);
    }

    [Fact()]
    public void CreateMap_FromCreateReservationToReservation_MapsCorrectly()
    {
        // arrange

        var command = new CreateReservationCommand
        {
            StarDate = DateTime.Now,
            EndDate = DateTime.Now,
            Status = "Status Test",
            CreatedAt = DateTime.Now,
            HotelId = 1,
            Notes = "Notes Test",
            AnimalId = 1,
            OwnerId = 1,
        };

        // act

        var reservation = _mapper.Map<Domain.Entities.Reservation>(command);

        // assert

        reservation.Should().NotBeNull();
        reservation.StarDate.Should().Be(command.StarDate);
        reservation.EndDate.Should().Be(command.EndDate);
        reservation.Status.Should().Be(command.Status);
        reservation.CreatedAt.Should().Be(command.CreatedAt);
        reservation.HotelId.Should().Be(command.HotelId);
        reservation.Notes.Should().Be(command.Notes);
        reservation.AnimalId.Should().Be(command.AnimalId);
        reservation.OwnerId.Should().Be(command.OwnerId);
    }
}