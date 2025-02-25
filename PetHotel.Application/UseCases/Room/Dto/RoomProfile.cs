using AutoMapper;

namespace PetHotel.Application.UseCases.Room.Dto;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Domain.Entities.Room, RoomDto>();
    }
}
