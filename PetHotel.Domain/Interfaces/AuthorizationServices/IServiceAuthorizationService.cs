using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces.AuthorizationServices;

public interface IServiceAuthorizationService
{
    bool Authorize(Service service, ResourceOperation resourceOperation);
}
