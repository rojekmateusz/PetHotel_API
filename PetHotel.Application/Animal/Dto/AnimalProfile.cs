using AutoMapper;


namespace PetHotel.Application.Animal.Dto;

public class AnimalProfile : Profile
{
    public AnimalProfile()
    {
        CreateMap<Domain.Entities.Animal, AnimalDto>()
            .ForMember(a => a.Reservations, opt => opt.MapFrom(src => src.Reservations));
    }
}
