using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces.AuthorizationServices;

public interface IReservationAuthorizationService
{
    bool Authorize(Reservation reservation, ResourceOperation resourceOperation);
}
