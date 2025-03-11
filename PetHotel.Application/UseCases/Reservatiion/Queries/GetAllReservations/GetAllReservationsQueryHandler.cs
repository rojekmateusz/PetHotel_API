using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Reservatiion.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Reservatiion.Queries.GetAllReservations;

public class GetAllReservationsQueryHandler(ILogger<GetAllReservationsQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : 
IRequestHandler<GetAllReservationsQuery, List<ReservationDto>>
{
    public async Task<List<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all reservations from Hotel {@HotelId}", request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());


        var reservations = mapper.Map<List<ReservationDto>>(hotel.Reservations);
        return reservations;

    }
}
