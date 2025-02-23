﻿using PetHotel.Application.Review.Dto;
using PetHotel.Application.Role.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.User.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<ReviewDto> Reviews { get; set; } = [];

    public List<RoleDto> Roles { get; set; } = [];
}
