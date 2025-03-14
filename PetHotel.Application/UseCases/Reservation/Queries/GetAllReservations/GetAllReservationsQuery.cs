using MediatR;
using PetHotel.Application.UseCases.Reservatiion.Dto;

namespace PetHotel.Application.UseCases.Reservatiion.Queries.GetAllReservations;

public class GetAllReservationsQuery(int hotelId) : IRequest<List<ReservationDto>>
{
    public int HotelId { get; set; } = hotelId;
}
