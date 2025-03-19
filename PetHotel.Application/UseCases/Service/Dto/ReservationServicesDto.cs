namespace PetHotel.Application.UseCases.Service.Dto;

public class ReservationServicesDto
{
    public int ReservationId { get; set; }
    public int ServiceId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}
