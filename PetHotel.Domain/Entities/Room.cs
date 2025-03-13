using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetHotel.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public int Capacity { get; set; } // Number of pets allowed in the room
    public decimal PricePerNight { get; set; }
    public string IsAvailable { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int HotelId { get; set; }

    public User user { get; set; } = default!;
    public string UserId { get; set; } = default!;
}
