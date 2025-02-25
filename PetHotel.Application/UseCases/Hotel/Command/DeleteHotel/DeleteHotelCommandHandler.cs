using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Hotel.Command.DeleteHotel;

public class DeleteHotelCommandHandler(ILogger<DeleteHotelCommandHandler> logger, IHotelRepository hotelRepository) : IRequestHandler<DeleteHotelCommand>
{
    public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting hotel with id {Id}", request.Id);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.Id);
        if (hotel is null)
        {
            throw new NotFoundException(nameof(hotel), request.Id.ToString());
        }
        await hotelRepository.DeleteHotel(hotel);
    }
}
