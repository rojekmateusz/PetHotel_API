using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Owner.Command.UpdateOwner;

public class UpdateOwnerCommandHandler(ILogger<UpdateOwnerCommandHandler> logger, IMapper mapper, IOwnerRepository ownerRepository,
   IOwnerAuthorizationService ownerAuthorizationService ) : IRequestHandler<UpdateOwnerCommand>
{
    public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update owner with Id: {@OwnerId} with {@UpdateOwner}", request.Id, request);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Owner), request.Id.ToString());

        if (!ownerAuthorizationService.Authorize(owner, ResourceOperation.Update))
            throw new ForbidException();
        
        mapper.Map(request, owner);
        await ownerRepository.SaveChanges();
    }
}
