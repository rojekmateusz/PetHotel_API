using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Owner.Command.DeleteOwner;

public class DeleteOwnerCommandHandler(ILogger<DeleteOwnerCommandHandler> logger, IOwnerRepository ownerRepository) : IRequest<DeleteOwnerCommand>
{
    public async Task DeleteOwner(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting owner with Id: {@OwnerId}", request.Id);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.Id);
        if (owner == null)
        {
            throw new NotFoundException(nameof(Owner), request.Id.ToString());
        }
        await ownerRepository.DeleteOwner(owner);
    }
}
