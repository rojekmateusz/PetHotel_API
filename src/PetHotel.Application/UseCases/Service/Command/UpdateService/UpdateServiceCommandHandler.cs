using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Service.Command.UpdateService;

public class UpdateServiceCommandHandler(ILogger<UpdateServiceCommandHandler> logger, IHotelRepository hotelRepository,
    IHotelAuthorizationService hotelAuthorizationService, IMapper mapper, IServiceRepository serviceRepository) : IRequestHandler<UpdateServiceCommand>
{
    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating service with id {Id}", request.ServiceId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        if(!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Update))
            throw new ForbidException();

        var service = hotel.Services.FirstOrDefault(s => s.ServiceId == request.ServiceId)
            ?? throw new NotFoundException(nameof(Service), request.ServiceId.ToString());

        mapper.Map(request, service);
        await serviceRepository.SaveChanges();
    }
}
