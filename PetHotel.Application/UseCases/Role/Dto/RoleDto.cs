using PetHotel.Application.UseCases.User.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.UseCases.Role.Dto;

public class RoleDto
{
    public int Id { get; set; }
    public string RoleName { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<UserDto> Users { get; set; } = [];
}
