using AutoMapper;

namespace PetHotel.Application.Owner.Dto;

public class OwnerProfile:Profile
{
    public OwnerProfile()
    {
        CreateMap<Domain.Entities.Owner, OwnerDto>()
            .ForMember(a => a.Animals, opt => opt.MapFrom(src => src.Animals))
            .ForMember(p => p.Payments, opt => opt.MapFrom(src => src.Payments));
    }
}
