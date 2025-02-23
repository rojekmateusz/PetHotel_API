namespace PetHotel.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<UserRole> UserRoles { get; set; } = [];
}
