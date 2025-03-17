using MediatR;
using PetHotel.Application.Common;
using PetHotel.Application.UseCases.Hotel.Dto;

namespace PetHotel.Application.UseCases.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQuery : IRequest<PagedResults<HotelDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
