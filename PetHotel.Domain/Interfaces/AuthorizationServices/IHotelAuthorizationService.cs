using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces.AuthorizationServices;

public interface IHotelAuthorizationService
{
    bool Authorize(Hotel hotel, ResourceOperation resourceOperation);
}
