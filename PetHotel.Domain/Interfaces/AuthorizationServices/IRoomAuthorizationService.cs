using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces.AuthorizationServices;

public interface IRoomAuthorizationService
{
    bool Authorize(Room room, ResourceOperation resourceOperation);
}
