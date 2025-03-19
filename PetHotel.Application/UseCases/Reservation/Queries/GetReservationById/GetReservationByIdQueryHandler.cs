using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Reservatiion.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Reservatiion.Queries.GetReservationById;

public class GetReservationByIdQueryHandler(ILogger<GetReservationByIdQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository,
    IReservationRepository reservationRepository) : IRequestHandler<GetReservationByIdQuery, ReservationDto>
{
    public async Task<ReservationDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting reservation by Id: {@ReservationId} from Hotel: {@HoteId}", request.ReservationId, request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var reservation = await reservationRepository.GetReservationByIdAsync(request.ReservationId)
            ?? throw new NotFoundException(nameof(Domain.Entities.Reservation), request.ReservationId.ToString());

        //var reservation = hotel.Reservations.FirstOrDefault(r => r.ReservationId == request.ReservationId)
        //     ?? throw new NotFoundException(nameof(Domain.Entities.Reservation), request.ReservationId.ToString());

        var result = mapper.Map<ReservationDto>(reservation);
        return result;

    }
}
