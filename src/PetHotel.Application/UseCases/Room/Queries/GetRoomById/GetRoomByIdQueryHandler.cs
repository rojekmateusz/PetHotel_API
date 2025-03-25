using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Room.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Room.Queries.GetRoomById;

public class GetRoomByIdQueryHandler(ILogger<GetRoomByIdQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<GetRoomByIdQuery, RoomDto>
{
    public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting room by Id: {@RoomId} in Hotel: {@HotelId}", request.RoomId, request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());
        var room = hotel.Rooms.FirstOrDefault(r => r.Id == request.RoomId)
            ?? throw new NotFoundException(nameof(Room), request.RoomId.ToString());
        var result = mapper.Map<RoomDto>(room);
        return result;
    }
}
