using AutoMapper;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Room.Command.CreateRoom;
using PetHotel.Application.UseCases.Room.Command.UpdateRoom;

namespace PetHotel.Application.UseCases.Room.Dto;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Domain.Entities.Room, RoomDto>();
        CreateMap<CreateRoomCommand, Domain.Entities.Room>();
        CreateMap<UpdateRoomCommand, Domain.Entities.Room>();
    }
}
