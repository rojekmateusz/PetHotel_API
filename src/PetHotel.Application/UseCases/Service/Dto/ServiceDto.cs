﻿using PetHotel.Application.UseCases.Reservation.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.UseCases.Service.Dto
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public List<ReservationService> ReservationServices { get; set; } = [];
    }
}
