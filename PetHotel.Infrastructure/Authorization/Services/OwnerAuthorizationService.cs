using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PetHotel.Application.User;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using System.Security.Claims;

namespace PetHotel.Infrastructure.Authorization.Services;

public class OwnerAuthorizationService(ILogger<OwnerAuthorizationService> logger, IUserContext userContext) : IOwnerAuthorizationService
{
    public bool Authorize(Owner owner, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorize user {UserId} with roles {Roles} to {ResourceOperation} for {Owner}",
            user?.Id,
            user?.Roles,
            resourceOperation,
            owner);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && user.Id == owner.UserId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        return false;
    }
}
