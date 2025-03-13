using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces;

public interface IOwnerAuthorizationService
{
    bool Authorize(Owner owner, ResourceOperation resourceOperation);
}
