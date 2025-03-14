using Microsoft.Extensions.Logging;
using PetHotel.Application.User;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Infrastructure.Authorization.Services
{
    public class HotelAuthorizationService(ILogger<HotelAuthorizationService> logger, IUserContext userContext) : IHotelAuthorizationService
    {
        public bool Authorize(Hotel hotel, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Authorize user {UserId} with roles {Roles} to {ResourceOperation} for {Owner}",
                user!.Id,
                user!.Roles,
                resourceOperation,
                hotel);

            if ((resourceOperation == ResourceOperation.Create) && user!.Id == hotel.UserId)
            {
                logger.LogInformation("Create operation - successful authorization");
                return true;
            }

            if ((resourceOperation == ResourceOperation.Delete) && user!.Id == hotel.UserId)
            {
                logger.LogInformation("Delete operation - successful authorization");
                return true;
            }

            if ((resourceOperation == ResourceOperation.Read) && user!.Id == hotel.UserId)
            {
                logger.LogInformation("Read operation - successful authorization");
                return true;
            }

            if ((resourceOperation == ResourceOperation.Update) && user!.Id == hotel.UserId)
            {
                logger.LogInformation("Update operation - successful authorization");
                return true;
            }

            return false;
        }

    }
}
