namespace PetHotel.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int HotelId { get; set; }

    public User user { get; set; } = default!;
    public string UserId { get; set; } = default!;
}
