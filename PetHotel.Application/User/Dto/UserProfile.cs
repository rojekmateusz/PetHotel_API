using AutoMapper;

namespace PetHotel.Application.User.Dto;

public class UserProfile: Profile
{
    public UserProfile() 
    {
        CreateMap<Domain.Entities.User, UserDto>()
            .ForMember(u => u.Roles, opt => opt.MapFrom(src => src.UserRoles))
            .ForMember(u => u.Reviews, opt => opt.MapFrom(src => src.Reviews));
    }
}
