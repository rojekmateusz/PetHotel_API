using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces.AuthorizationServices;

public interface IAnimalAuthorizationService
{
    bool Authorize(Animal animal, ResourceOperation resourceOperation);
}

