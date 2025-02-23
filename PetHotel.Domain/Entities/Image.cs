namespace PetHotel.Domain.Entities;

public class Image
{
    public int Id { get; set; }
    public string Url { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int HotelId { get; set; }
}
