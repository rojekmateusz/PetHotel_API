using MediatR;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.UseCases.Service.Command.UpdateService;

public class UpdateServiceCommand : IRequest
{
    public int ServiceId { get; set; }
    public int HotelId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
