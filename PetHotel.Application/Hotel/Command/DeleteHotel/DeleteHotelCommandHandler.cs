using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Hotel.Command.DeleteHotel;

public class DeleteHotelCommandHandler(ILogger<DeleteHotelCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<DeleteHotelCommand>
{
    public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting hotel with id {Id}", request.Id);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.Id);
        await hotelRepository.DeleteHotel(hotel);
    }
}
