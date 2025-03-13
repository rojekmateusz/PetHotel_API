namespace PetHotel.Domain.Entities;

public class Reservation
{
    public int ReservationId { get; set; }
    public DateTime StarDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Notes { get; set; }
    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public int AnimalId { get; set; }
    public Animal? Animal { get; set; }

    public List<ReservationService> ReservationServices { get; set; } = [];

    public User user { get; set; } = default!;
    public string UserId { get; set; } = default!;
}
