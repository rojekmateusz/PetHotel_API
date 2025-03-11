
using Microsoft.AspNetCore.Identity;
using PetHotel.Domain.Constants;
using PetHotel.Infrastructure.Persistance;
using System.Runtime.CompilerServices;

namespace PetHotel.Infrastructure.Seeders;

internal class Seeder(PetHotelDbContext dbContext) : ISeeder
{
    public async Task Seed()
    {
        if(!dbContext.Roles.Any())
        {
            var roles = GetRoles();
            dbContext.Roles.AddRange(roles);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new(UserRoles.User)
                { 
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new(UserRoles.Owner)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new(UserRoles.Admin)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                }
            ];
        return roles;
    }
}
