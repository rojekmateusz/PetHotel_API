using MediatR;
using PetHotel.Application.UseCases.Room.Dto;

namespace PetHotel.Application.UseCases.Room.Queries.GetAllRoomsByHotelId;

public class GetAllRoomsByHotelIdQuery(int hotelId): IRequest<IEnumerable<RoomDto>>
{
    public int HotelId { get; set; } = hotelId;
}
