using MediatR;
using PetHotel.Application.Image.Dto;
using PetHotel.Application.Reservatiion.Dto;
using PetHotel.Application.Review.Dto;
using PetHotel.Application.Room.Dto;

namespace PetHotel.Application.Hotel.Command.CreateHotel;

public class CreateHotelCommand: IRequest<int>
{
    public string HotelName { get; set; } = default!;
    public int HotelsNIP { get; set; } = default!;
    public string HotelOwnerName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Description { get; set; }
    public string IsActive { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
