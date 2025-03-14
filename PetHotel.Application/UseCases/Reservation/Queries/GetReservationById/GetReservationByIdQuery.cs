using MediatR;
using PetHotel.Application.UseCases.Reservatiion.Dto;

namespace PetHotel.Application.UseCases.Reservatiion.Queries.GetReservationById;

public class GetReservationByIdQuery(int hotelId, int reservationId) : IRequest<ReservationDto>
{
    public int Id { get; set; } = reservationId;
    public int HotelId { get; set; } = hotelId;
}
