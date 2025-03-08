using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Reservatiion.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Reservatiion.CreateReservation;

public class CreateReservationCommandHandler(ILogger<CreateReservationCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository, 
    IReservationRepository reservationRepository) : IRequestHandler<CreateReservationCommand, int>
{
    public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating reservation for hotel {HotelId}", request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var reservation = mapper.Map<Domain.Entities.Reservation>(request);
        int id = await reservationRepository.CreateReservation(reservation);
        return id;


    }
}
