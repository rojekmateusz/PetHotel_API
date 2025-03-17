using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.Common;
using PetHotel.Application.UseCases.Hotel.Dto;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQueryHandler(ILogger<GetAllHotelsQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<GetAllHotelsQuery, PagedResults<HotelDto>>
{
    public async Task<PagedResults<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Hotels");
        
        var (hotels, totalCount) = await hotelRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageNumber);

        var hotelDto = mapper.Map<IEnumerable<HotelDto>>(hotels);
        var result = new PagedResults<HotelDto>(hotelDto, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
