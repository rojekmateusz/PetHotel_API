using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Interfaces.AuthorizationServices;

public interface IImageAuthorizationService
{
    bool Authorize(Image image, ResourceOperation resourceOperation);
}
