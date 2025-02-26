using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Room.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Room.Queries.GetAllRoomsByHotelId;

public class GetAllRoomsByHotelIdQueryHandler(ILogger<GetAllRoomsByHotelIdQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) :
    IRequestHandler<GetAllRoomsByHotelIdQuery, IEnumerable<RoomDto>>
{
    public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all rooms in hotel {@HotelId}", request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());
        var results = mapper.Map<IEnumerable<RoomDto>>(hotel.Rooms);
        return results;
    }
}
