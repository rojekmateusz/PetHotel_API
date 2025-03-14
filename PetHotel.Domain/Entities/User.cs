using Microsoft.AspNetCore.Identity;
using PetHotel.Domain.Constants;

namespace PetHotel.Domain.Entities;

public class User : IdentityUser
{
    public Owner? OwnedOwner { get; set; }
    public List<Hotel>? OwnedHotels { get; set; }
   
}
