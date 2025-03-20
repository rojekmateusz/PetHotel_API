using PetHotel.Application.UseCases.Reservation.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.UseCases.Animal.Dto;

public class AnimalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Spiecies { get; set; } = default!;
    public string Breed { get; set; } = default!;
    public int? Age { get; set; }
    public string? Note { get; set; }
    public decimal? Weight { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<ReservationDto> Reservations { get; set; } = [];

}
