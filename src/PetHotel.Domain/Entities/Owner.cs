namespace PetHotel.Domain.Entities;

public class Owner
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Animal> Animals { get; set; } = [];
    public List<Payment> Payments { get; set; } = [];
    public List<Reservation> Reservations { get; set; } = [];

    public User user { get; set; } = default!;
    public string UserId { get; set; } = default!;
}
