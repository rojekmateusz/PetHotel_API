using MediatR;

namespace PetHotel.Application.UseCases.Room.Command.CreateRoom;

public class CreateRoomCommand: IRequest<int>
{
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public string IsAvailable { get; set; } = default!;
    public int HotelId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
