using MediatR;

namespace PetHotel.Application.UseCases.Room.Command.UpdateRoom;

public class UpdateRoomCommand: IRequest
{
    public int Id { get; set; }
    public int Capacity { get; set; } // Number of pets allowed in the room
    public decimal PricePerNight { get; set; }
    public string IsAvailable { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int HotelId { get; set; }
}
