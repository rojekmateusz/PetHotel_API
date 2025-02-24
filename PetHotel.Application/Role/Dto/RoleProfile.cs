using AutoMapper;

namespace PetHotel.Application.Role.Dto;

public class RoleProfile: Profile
{
    public RoleProfile()
    {
        CreateMap<Domain.Entities.Role, RoleDto>()
            .ForMember(u => u.Users, opt => opt.MapFrom(src => src.UserRoles));
    }
}
