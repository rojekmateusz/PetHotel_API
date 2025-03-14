using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;

public class UpdateHotelCommandHandler(ILogger<UpdateHotelCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository,
    IHotelAuthorizationService authorizationService) : IRequestHandler<UpdateHotelCommand>
{
    public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating hotel with Id: {@HotelId} with {@UpdatedHotel}", request.Id, request);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Hotel), request.Id.ToString());

        if (!authorizationService.Authorize(hotel, ResourceOperation.Update))
            throw new ForbidException();
        
        mapper.Map(request, hotel);
        await hotelRepository.SaveChanges();
    }
}
