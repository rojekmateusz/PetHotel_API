namespace PetHotel.Domain.Entities;

public class Hotel
{
    public int Id { get; set; }
    public string HotelName { get; set; } = default!;
    public int HotelsNIP { get; set; } = default!;
    public string HotelOwnerName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Email { get; set; } = default!;
    public  string? Description { get; set; }
    public int? Rating { get; set; } 
    public string IsActive { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Image> Images { get; set; } = [];
    public List<Reservation> Reservations { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
    public List<Room> Rooms { get; set; } = [];

}
