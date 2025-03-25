
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;
using PetHotel.Infrastructure.Persistance;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PetHotel.Infrastructure.Seeders;

internal class Seeder(PetHotelDbContext dbContext, UserManager<User> userManager) : ISeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = Roles;
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any())
            {
                await SeedUsers();
            }
        }
    }

    private static IEnumerable<IdentityRole> Roles
    {
        get
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
    }

    private async Task<IEnumerable<IdentityResult>> SeedUsers()
    {
        var users = new List<IdentityResult>();

        string adminpswd = "Admin123!";

        var admin = new User
        {
            UserName = "admin@pethotel.com",
            Email = "admin@pethotel.com",
        };
        var resultAdmin = await userManager.CreateAsync(admin, adminpswd);
        await userManager.AddToRoleAsync(admin, UserRoles.Admin);
        users.Add(resultAdmin);


        string user1pswd = "Password1!";

        var user1 = new User
        {
            UserName = "user@pethotel.com",
            Email = "user@pethotel.com",
        };
        var resultUser1 = await userManager.CreateAsync(user1, user1pswd);
        await userManager.AddToRoleAsync(user1, UserRoles.User);
        users.Add(resultUser1);

        string user2pswd = "Password2!";

        var user2 = new User
        {
            UserName = "user2@pethotel.com",
            Email = "user2@pethotel.com",
        };
        var resultUser2 = await userManager.CreateAsync(user2, user2pswd);
        await userManager.AddToRoleAsync(user2, UserRoles.User);
        users.Add(resultUser2);

        return users;

    }
}
