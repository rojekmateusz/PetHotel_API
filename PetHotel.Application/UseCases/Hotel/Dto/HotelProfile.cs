using AutoMapper;
using PetHotel.Application.UseCases.Hotel.Command.CreateHotel;
using PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;

namespace PetHotel.Application.UseCases.Hotel.Dto;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<Domain.Entities.Hotel, HotelDto>()
            .ForMember(h => h.Rooms, opt => opt.MapFrom(src => src.Rooms))
            .ForMember(i => i.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(r => r.Reservations, opt => opt.MapFrom(scr => scr.Reservations))
            .ForMember(r => r.Reviews, opt => opt.MapFrom(src => src.Reviews));

        CreateMap<CreateHotelCommand, Domain.Entities.Hotel>();
        CreateMap<UpdateHotelCommand, Domain.Entities.Hotel>();
    }
}
