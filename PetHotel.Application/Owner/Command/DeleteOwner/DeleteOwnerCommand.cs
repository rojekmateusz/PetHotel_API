using MediatR;

namespace PetHotel.Application.Owner.Command.DeleteOwner;

public class DeleteOwnerCommand(int id): IRequest
{
    public int Id { get; set; } = id;
}
