using MediatR;

namespace PetHotel.Application.UseCases.Reservation.Command.DeleteReservation;

public class DeleteReservationCommand(int hotelId, int reservationId) : IRequest
{
    public int HotelId { get; set; } = hotelId;
    public int ReservationId { get; set; } = reservationId;
}
