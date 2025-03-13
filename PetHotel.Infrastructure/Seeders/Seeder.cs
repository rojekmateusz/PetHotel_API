
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;
using PetHotel.Infrastructure.Persistance;
using System.Runtime.CompilerServices;

namespace PetHotel.Infrastructure.Seeders;

internal class Seeder(PetHotelDbContext dbContext, UserManager<User> userManager) : ISeeder
{
    public async Task Seed()
    {
        if (!dbContext.Roles.Any())
        {
            var roles = GetRoles();
            dbContext.Roles.AddRange(roles);
            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.Users.Any())
        {
            await SeedAdminUser();
            await SeedUser();
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

                new(UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                }
            ];
        return roles;
    }

    private async Task<IdentityResult> SeedAdminUser()
    {

        string password = "Admin123!";

        var user = new User
        {
            UserName = "admin@pethotel.com",
            Email = "admin@pethotel.com",
        };
        var result = await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, UserRoles.Admin);

        return result;

    }

    private async Task<IdentityResult> SeedUser()
    {

        string password = "Password1!";

        var user = new User
        {
            UserName = "user@pethotel.com",
            Email = "user@pethotel.com",
        };
        var result = await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, UserRoles.User);

        return result;

    }
}
