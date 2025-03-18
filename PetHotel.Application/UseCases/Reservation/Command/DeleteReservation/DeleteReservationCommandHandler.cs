using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Reservation.Command.DeleteReservation;

public class DeleteReservationCommandHandler(ILogger<DeleteReservationCommandHandler> logger, IHotelRepository hotelRepository, 
    IReservationRepository reservationRepository, IHotelAuthorizationService hotelAuthorizationService) : IRequestHandler<DeleteReservationCommand>
{
    public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting reservation with id {ReservationId} from hotel with id {HotelId}", request.ReservationId, request.HotelId);

        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        if(!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Delete))
            throw new ForbidException();

        var service = hotel.Reservations.FirstOrDefault(r => r.ReservationId == request.ReservationId)
            ?? throw new NotFoundException(nameof(Reservation), request.ReservationId.ToString());

        await reservationRepository.DeleteReservation(service);
    }
}
