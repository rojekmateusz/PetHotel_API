using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Reservatiion.Dto;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using System.Reflection;

namespace PetHotel.Application.UseCases.Reservatiion.Queries.GetReservationById;

public class GetReservationByIdQueryHandler(ILogger<GetReservationByIdQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository,
    IReservationRepository reservationRepository, IHotelAuthorizationService hotelAuthorizationService,
    IOwnerAuthorizationService ownerAuthorizationService, IOwnerRepository ownerRepository) : IRequestHandler<GetReservationByIdQuery, ReservationDto>
{
    public async Task<ReservationDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting reservation by Id: {@ReservationId} from Hotel: {@HoteId}", request.ReservationId, request.HotelId);
        
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var reservation = await reservationRepository.GetReservationByIdAsync(request.ReservationId)
            ?? throw new NotFoundException(nameof(Domain.Entities.Reservation), request.ReservationId.ToString());

        var owner = await ownerRepository.GetOwnerByIdAsync(reservation.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), reservation.OwnerId.ToString());
                
       
        bool isAuthorized = hotelAuthorizationService.Authorize(hotel, ResourceOperation.Read) || 
                           ownerAuthorizationService.Authorize(owner, ResourceOperation.Read);

        if (!isAuthorized)
            throw new ForbidException();

        var result = mapper.Map<ReservationDto>(reservation);
        return result;
    }
}
