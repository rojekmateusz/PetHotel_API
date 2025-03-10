using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Service.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Service.Queries.GetAllServices;

public class GetAllServicesQueryHandler(ILogger<GetAllServicesQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceDto>>
{
    public async Task<IEnumerable<ServiceDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation(" Getting all services for Hotel: {HotelId}", request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());
        var results = mapper.Map<IEnumerable<ServiceDto>>(hotel.Services);
        return results;
    }
}
