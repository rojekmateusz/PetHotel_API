using PetHotel.Application.Image.Dto;
using PetHotel.Application.Reservatiion.Dto;
using PetHotel.Application.Review.Dto;
using PetHotel.Application.Room.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.Hotel.Dto;

public class HotelDto
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
    public int? Rating { get; set; }
    public string IsActive { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<ImageDto> Images { get; set; } = [];
    public List<ReservationDto> Reservations { get; set; } = [];
    public List<ReviewDto> Reviews { get; set; } = [];
    public List<RoomDto> Rooms { get; set; } = [];
}
