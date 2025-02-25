using MediatR;
using PetHotel.Application.UseCases.Hotel.Dto;

namespace PetHotel.Application.UseCases.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQuery : IRequest<IEnumerable<HotelDto>>
{

}
