using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Hotel.Command.UpdateHotel;

public class UpdateHotelCommandHandler(ILogger<UpdateHotelCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<UpdateHotelCommand>
{
    public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating hotel with Id: {@HotelId} with {@UpdatedHotel}", request.Id, request);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.Id);
        if (hotel == null)
        {
            throw new NotFoundException(nameof(Hotel), request.Id.ToString());
        }
        mapper.Map(request, hotel);
        await hotelRepository.SaveChanges();
    }
}
