using MediatR;

namespace PetHotel.Application.UseCases.Owner.Command.DeleteOwner;

public class DeleteOwnerCommand(int ownerId) : IRequest
{
    public int Id { get; set; } = ownerId;
}
