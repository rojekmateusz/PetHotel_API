using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.Hotel.Dto;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQueryHandler(ILogger<GetAllHotelsQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<GetAllHotelsQuery, IEnumerable<HotelDto>>
{
    public async Task<IEnumerable<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Hotels");
        var hotels = await hotelRepository.GetAllHotelsAsync();
        var hotelDto = mapper.Map<IEnumerable<HotelDto>>(hotels);
        return hotelDto;
    }
}
