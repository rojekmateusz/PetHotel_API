using MediatR;
using PetHotel.Application.Hotel.Dto;

namespace PetHotel.Application.Hotel.Queries.GetHotelById;

public class GetHotelByIdQuery(int id) : IRequest<HotelDto>
{
    public int Id { get; set; } = id;
}
