using MediatR;

namespace PetHotel.Application.UseCases.Service.Command.CreateService;

public class CreateServiceCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public int HotelId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
}
