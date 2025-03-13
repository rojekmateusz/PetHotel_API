using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces.AuthorizationServices;

public interface IReviewAuthorizationService
{
    bool Authorize(Review review, ResourceOperation resourceOperation);
}
