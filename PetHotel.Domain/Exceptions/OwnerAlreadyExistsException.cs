namespace PetHotel.Domain.Exceptions;

public class OwnerAlreadyExistsException(string resourceIdentifier) : Exception($" User with Id: {resourceIdentifier} already has an assigned owner")
{
}