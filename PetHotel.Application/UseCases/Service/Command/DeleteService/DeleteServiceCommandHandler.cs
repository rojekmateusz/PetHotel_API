using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Service.Command.DeleteService;

public class DeleteServiceCommandHandler(ILogger<DeleteServiceCommandHandler> logger, IServiceRepository serviceRepository, IHotelRepository hotelRepository,
    IHotelAuthorizationService hotelAuthorization) : IRequestHandler<DeleteServiceCommand>
{
    public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting service with ID {request.ServiceId} from hotel with ID {request.HotelId}");
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        if (!hotelAuthorization.Authorize(hotel, ResourceOperation.Delete))
            throw new ForbidException();

        var service = hotel.Services.FirstOrDefault(s => s.ServiceId == request.ServiceId)
            ?? throw new NotFoundException(nameof(Service), request.ServiceId.ToString());

        await serviceRepository.DeleteService(service);
    }
}
