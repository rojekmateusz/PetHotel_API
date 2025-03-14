using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Service.Command.CreateService;

public class CreateServiceCommandHandler(ILogger<CreateServiceCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository,
    IServiceRepository serviceRepository, IHotelAuthorizationService hotelAuthorizationService) : IRequestHandler<CreateServiceCommand, int>
{
    public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating service for Hotel: {@HotelId}", request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        if(!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Create))
            throw new ForbidException();

        var service = mapper.Map<Domain.Entities.Service>(request);
        return await serviceRepository.CreateService(service);
       
    }
}
