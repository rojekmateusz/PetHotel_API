using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;
using System.Data;

namespace PetHotel.Application.UseCases.Room.Command.DeleteRoom;

public class DeleteRoomCommandHandler(ILogger<DeleteRoomCommandHandler> logger, IHotelRepository hotelRepository, IRoomRepository roomRepository) :
    IRequestHandler<DeleteRoomCommand>
{
    public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting room with Id: {@RoomId} in Hotel: {@HotelId}", request.RoomID, request.HotelID);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelID)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelID.ToString());
        var room = hotel.Rooms.FirstOrDefault(r => r.Id == request.RoomID)
            ?? throw new NotFoundException(nameof(Room), request.RoomID.ToString());
        await roomRepository.DeleteRoom(room);
    }
}
