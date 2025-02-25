using AutoMapper;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;


namespace PetHotel.Application.UseCases.Animal.Dto;

public class AnimalProfile : Profile
{
    public AnimalProfile()
    {
        CreateMap<Domain.Entities.Animal, AnimalDto>()
           .ForMember(a => a.Reservations, opt => opt.MapFrom(src => src.Reservations));

        CreateMap<CreateAnimalCommand, Domain.Entities.Animal>();


    }
}
