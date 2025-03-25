using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Room.Command.CreateRoom;

public class CreateRoomCommandHandler(ILogger<CreateRoomCommandHandler> logger, IMapper mapper, IRoomRepository roomRepository,
    IHotelRepository hotelRepository, IHotelAuthorizationService hotelAuthorizationService) : IRequestHandler<CreateRoomCommand, int>
{
    public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create Room  in Hotel {HotelId}", request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        if(!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Create))
            throw new ForbidException();

        var room = mapper.Map<Domain.Entities.Room>(request);
        return await roomRepository.CreateRoom(room);
    }
}
