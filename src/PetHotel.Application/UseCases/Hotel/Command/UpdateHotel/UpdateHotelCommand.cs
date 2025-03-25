using MediatR;

namespace PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;

public class UpdateHotelCommand : IRequest
{
    public int Id { get; set; }
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
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
