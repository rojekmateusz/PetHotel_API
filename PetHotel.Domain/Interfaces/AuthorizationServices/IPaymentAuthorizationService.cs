using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces.AuthorizationServices;

public interface IPaymentAuthorizationService
{
    bool Authorize(Payment payment, ResourceOperation resourceOperation);
}
