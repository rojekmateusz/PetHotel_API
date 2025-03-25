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
            user!.Id,
            user!.Roles,
            resourceOperation,
            owner);

        if ((resourceOperation == ResourceOperation.Create) && user!.Id == owner.UserId)
        {
            logger.LogInformation("Create operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete ) && user!.Id == owner.UserId)
        {
            logger.LogInformation("Delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Read) && user!.Id == owner.UserId)
        {
            logger.LogInformation("Read operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Update) && user!.Id == owner.UserId)
        {
            logger.LogInformation("Update operation - successful authorization");
            return true;
        }

        return false;
    }
}
