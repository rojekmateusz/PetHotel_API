using Microsoft.AspNetCore.Identity;
using PetHotel.Domain.Constants;

namespace PetHotel.Domain.Entities;

public class User : IdentityUser
{
    public Owner? OwnedOwner { get; set; }
    public List<Animal>? OwnedAnimals { get; set; }
    public List<Reservation>? OwnedReservations { get; set; }
    public List<Hotel>? OwnedHotels { get; set; }
    public List<Room>? OwnedRooms { get; set; }
    public List<Image>? OwnedImages { get; set; }
    public List<Review>? OwnedReviews { get; set; }
    public List<Payment>? OwnedPayments { get; set; }
    public List<Service>? OwnedServices { get; set; }
}
