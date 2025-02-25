using MediatR;
using PetHotel.Application.Hotel.Dto;

namespace PetHotel.Application.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQuery: IRequest<IEnumerable<HotelDto>>
{

}
