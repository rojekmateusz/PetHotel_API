using MediatR;

namespace PetHotel.Application.UseCases.Room.Command.DeleteRoom;

public class DeleteRoomCommand(int roomId, int hotelId) : IRequest
{
    public int RoomID { get; set; } = roomId;
    public int HotelID { get; set; } = hotelId;
}
