using Microsoft.Extensions.Logging;
using PetHotel.Application.User;

namespace PetHotel.Infrastructure.Authorization.Services;

public class AnimalAuthorizationService(ILogger<OwnerAuthorizationService> logger, IUserContext userContext)  
{
}
