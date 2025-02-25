using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Hotel.Command.CreateHotel;

public class CreateHotelCommandHandler(ILogger<CreateHotelCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<CreateHotelCommand, int>
{
    public async Task<int> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create Hotel {@Hotel}", request);
        var hotel = mapper.Map<Domain.Entities.Hotel>(request);
        int id = await hotelRepository.CreateHotel(hotel);
        return id;
    }
}
