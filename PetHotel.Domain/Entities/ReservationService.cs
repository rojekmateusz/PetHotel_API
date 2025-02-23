namespace PetHotel.Domain.Entities;

public class ReservationService
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int ReservationId { get; set; }
    public Reservation? Reservation { get; set; } 
    public List<Service> Services { get; set; } = [];
}
