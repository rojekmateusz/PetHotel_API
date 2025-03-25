using MediatR;
using PetHotel.Application.UseCases.Room.Dto;

namespace PetHotel.Application.UseCases.Room.Queries.GetRoomById;

public class GetRoomByIdQuery(int hotelId, int roomId) : IRequest<RoomDto>
{
    public int HotelId { get; set; } = hotelId;
    public int RoomId { get; set;} = roomId;
}
