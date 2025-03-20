using MediatR;
using PetHotel.Application.UseCases.Reservation.Dto;

namespace PetHotel.Application.UseCases.Reservation.Queries.GetAllReservations;

public class GetAllReservationsQuery(int hotelId) : IRequest<List<ReservationDto>>
{
    public int HotelId { get; set; } = hotelId;
}
