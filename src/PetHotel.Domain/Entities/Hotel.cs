﻿namespace PetHotel.Domain.Entities;

public class Hotel
{
    public int Id { get; set; }
    public string HotelName { get; set; } = default!;
    public string HotelsNIP { get; set; } = default!;
    public string HotelOwnerName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Email { get; set; } = default!;
    public  string Description { get; set; } = default!;
    public double? AverageRating { get; set; }
    public string IsActive { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Image> Images { get; set; } = [];
    public List<Reservation> Reservations { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
    public List<Room> Rooms { get; set; } = [];
    public List<Service> Services { get; set; } = [];

    public User User { get; set; } = default!;
    public string UserId { get; set; } = default!;

    public double? CalculateAverageRating()
    {
        if (Reviews == null || Reviews.Count == 0)
            return null;

        double avgRating =  Reviews.Average(r => r.Rating);
        return Math.Round(avgRating, 1);
    }
}
