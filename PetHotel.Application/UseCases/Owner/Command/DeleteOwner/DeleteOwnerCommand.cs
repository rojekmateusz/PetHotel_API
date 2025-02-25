using MediatR;

namespace PetHotel.Application.UseCases.Owner.Command.DeleteOwner;

public class DeleteOwnerCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
