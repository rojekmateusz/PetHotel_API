namespace PetHotel.Application.UseCases.Room.Dto;

public class RoomDto
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public string IsAvailable { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
