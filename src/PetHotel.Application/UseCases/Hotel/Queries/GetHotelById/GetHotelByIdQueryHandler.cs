using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Hotel.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Hotel.Queries.GetHotelById;

public class GetHotelByIdQueryHandler(ILogger<GetHotelByIdQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<GetHotelByIdQuery, HotelDto>
{
    public async Task<HotelDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation(" Getting Hotel by Id: {Id}", request.Id);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Hotel), request.Id.ToString());
        var hotelDto = mapper.Map<HotelDto>(hotel);
        return hotelDto;
    }
}
