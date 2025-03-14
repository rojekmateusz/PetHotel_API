using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Hotel.Command.DeleteHotel;

public class DeleteHotelCommandHandler(ILogger<DeleteHotelCommandHandler> logger, IHotelRepository hotelRepository,
    IHotelAuthorizationService hotelAuthorizationService) : IRequestHandler<DeleteHotelCommand>
{
    public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting hotel with id {Id}", request.Id);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Hotel), request.Id.ToString());

        if(!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Delete))
            throw new ForbidException();

        await hotelRepository.DeleteHotel(hotel);
    }
}
