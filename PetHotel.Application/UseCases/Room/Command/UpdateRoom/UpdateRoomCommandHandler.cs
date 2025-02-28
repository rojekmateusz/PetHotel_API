﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Room.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Room.Command.UpdateRoom;

public class UpdateRoomCommandHandler(ILogger<UpdateRoomCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository, IRoomRepository roomRepository) : IRequestHandler<UpdateRoomCommand>
{
    public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating room with Id: {@RoomId} in Hotel {@HotelId}", request.Id, request.HotelId);
        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());
        var room = hotel.Rooms.FirstOrDefault(r => r.Id==request.Id)
            ?? throw new NotFoundException(nameof(Room), request.Id.ToString());
        mapper.Map(request, room);
        await roomRepository.SaveChanges();
    }
}
