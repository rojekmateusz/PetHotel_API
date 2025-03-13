using Microsoft.Extensions.Logging;
using PetHotel.Application.User;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Interfaces.AuthorizationServices;

namespace PetHotel.Infrastructure.Authorization.Services;

public class AnimalAuthorizationService(ILogger<AnimalAuthorizationService> logger, IUserContext userContext) : IAnimalAuthorizationService
{
    public bool Authorize(Animal animal, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorize user {UserId} with roles {Roles} to {ResourceOperation} for {Owner}",
            user?.Id,
            user?.Roles,
            resourceOperation,
            animal);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.User))
        {
            logger.LogInformation("User delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && user.Id == animal.UserId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        return false;
    }
}
