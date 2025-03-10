using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Service.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Service.Queries.GetServiceByID;

public class GetServiceByIdQueryHandler(ILogger<GetServiceByIdQueryHandler> logger, IMapper mapper, IHotelRepository hotelRepository) : IRequestHandler<GetServiceByIdQuery, ServiceDto>
{
    public async Task<ServiceDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting service by Id: {@ServiceId} in Hotel: {@HotelId}", request.Id, request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());
        var service = hotel.Services.FirstOrDefault(s => s.Id == request.Id)
            ?? throw new NotFoundException(nameof(Service), request.Id.ToString());
        var result = mapper.Map<ServiceDto>(service);
        return result;
    }
}
