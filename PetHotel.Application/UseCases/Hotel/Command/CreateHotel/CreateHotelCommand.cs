using MediatR;

namespace PetHotel.Application.UseCases.Hotel.Command.CreateHotel;

public class CreateHotelCommand : IRequest<int>
{
    public string HotelName { get; set; } = default!;
    public string HotelsNIP { get; set; } = default!;
    public string HotelOwnerName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Description { get; set; }
    public string IsActive { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
