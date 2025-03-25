using MediatR;
using PetHotel.Application.UseCases.Hotel.Dto;

namespace PetHotel.Application.UseCases.Hotel.Queries.GetHotelById;

public class GetHotelByIdQuery(int hotelId) : IRequest<HotelDto>
{
    public int Id { get; set; } = hotelId;
}
