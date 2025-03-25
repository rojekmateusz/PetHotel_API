using MediatR;
using PetHotel.Application.UseCases.Reservation.Dto;

namespace PetHotel.Application.UseCases.Reservation.Queries.GetReservationById;

public class GetReservationByIdQuery(int hotelId, int reservationId) : IRequest<ReservationDto>
{
    public int ReservationId { get; set; } = reservationId;
    public int HotelId { get; set; } = hotelId;
    
}
