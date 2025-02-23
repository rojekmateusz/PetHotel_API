using PetHotel.Application.Service.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.Reservatiion.Dto;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime StarDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
     
    public List<ServiceDto> Services { get; set; } = [];
}
