using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Reservation.Command.DeleteReservation;

public class DeleteReservationCommandHandler(ILogger<DeleteReservationCommandHandler> logger, IHotelRepository hotelRepository, 
    IReservationRepository reservationRepository, IHotelAuthorizationService hotelAuthorizationService,
    IOwnerAuthorizationService ownerAuthorizationService, IOwnerRepository ownerRepository) : IRequestHandler<DeleteReservationCommand>
{
    public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting reservation with id {ReservationId} from hotel with id {HotelId}", request.ReservationId, request.HotelId);

        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var reservation = await reservationRepository.GetReservationByIdAsync(request.ReservationId)
            ?? throw new NotFoundException(nameof(Domain.Entities.Reservation), request.ReservationId.ToString());

        var owner = await ownerRepository.GetOwnerByIdAsync(reservation.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), reservation.OwnerId.ToString());

        bool isAuthorized = hotelAuthorizationService.Authorize(hotel, ResourceOperation.Delete) || 
                           ownerAuthorizationService.Authorize(owner, ResourceOperation.Delete);

        if (!isAuthorized)
            throw new ForbidException();

        await reservationRepository.DeleteReservation(reservation);
    }
}
