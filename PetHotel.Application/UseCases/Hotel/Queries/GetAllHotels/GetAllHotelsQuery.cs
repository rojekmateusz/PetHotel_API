using MediatR;
using PetHotel.Application.Common;
using PetHotel.Application.UseCases.Hotel.Dto;
using PetHotel.Domain.Constants;

namespace PetHotel.Application.UseCases.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQuery : IRequest<PagedResults<HotelDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
