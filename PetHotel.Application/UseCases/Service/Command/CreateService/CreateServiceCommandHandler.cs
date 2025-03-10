using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Service.Command.CreateService;

public class CreateServiceCommandHandler(ILogger<CreateServiceCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository,
    IServiceRepository serviceRepository) : IRequestHandler<CreateServiceCommand, int>
{
    public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating service for Hotel: {@HotelId}", request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var service = mapper.Map<Domain.Entities.Service>(request);
        return await serviceRepository.CreateService(service);
       
    }
}
