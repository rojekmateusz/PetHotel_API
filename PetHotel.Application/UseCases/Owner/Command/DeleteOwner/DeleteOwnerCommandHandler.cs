using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Owner.Command.DeleteOwner;

public class DeleteOwnerCommandHandler(ILogger<DeleteOwnerCommandHandler> logger, IOwnerRepository ownerRepository,
    IOwnerAuthorizationService ownerAuthorizationService) : IRequest<DeleteOwnerCommand>
{
    public async Task DeleteOwner(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting owner with Id: {@OwnerId}", request.Id);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Owner), request.Id.ToString());

        if (!ownerAuthorizationService.Authorize(owner, ResourceOperation.Delete))
            throw new ForbidException();

        await ownerRepository.DeleteOwner(owner);
    }
}
