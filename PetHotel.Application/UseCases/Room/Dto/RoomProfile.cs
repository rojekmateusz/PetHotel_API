using AutoMapper;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Room.Command.CreateRoom;

namespace PetHotel.Application.UseCases.Room.Dto;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Domain.Entities.Room, RoomDto>();
        CreateMap<CreateRoomCommand, Domain.Entities.Room>();
    }
}
