﻿using PetHotel.Application.UseCases.Service.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.UseCases.Reservatiion.Dto;

public class ReservationDto
{
    public int ReservationId { get; set; }
    public DateTime StarDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Notes { get; set; }
    public int AnimalId { get; set; }

    public List<ReservationService> ReservationServices { get; set; } = [];
}
