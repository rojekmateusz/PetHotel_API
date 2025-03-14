using MediatR;
using PetHotel.Application.UseCases.Reservatiion.Dto;
using PetHotel.Application.UseCases.Service.Dto;

namespace PetHotel.Application.UseCases.Reservation.Command.CreateReservation;

public class CreateReservationCommand : IRequest<int>
{
    public DateTime StarDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string Status { get; set; } = "Confirmed";
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int HotelId { get; set; }
    public int AnimalId { get; set; }
    public List<int> servicesIds { get; set; } = [];
}
