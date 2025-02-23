﻿namespace PetHotel.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public DateTime StarDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public int AnimalId { get; set; }
    public Animal? Animal { get; set; } 

    public ReservationService? ReservationService { get; set; }
}
