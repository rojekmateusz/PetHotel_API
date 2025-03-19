using PetHotel.Application.UseCases.Service.Dto;
using PetHotel.Domain.Entities;
using System.Text.Json.Serialization;

namespace PetHotel.Application.UseCases.Reservatiion.Dto;

public class ReservationDto
{
    public int ReservationId { get; set; }
    public DateTime StarDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int HotelId { get; set; }
    public string? Notes { get; set; }
    public int AnimalId { get; set; }
    public int OwnerId { get; set; }

    
    public List<ReservationServicesDto> ReservationServices { get; set; } = [];
    
}
