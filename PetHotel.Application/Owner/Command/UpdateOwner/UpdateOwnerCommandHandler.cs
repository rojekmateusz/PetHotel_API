using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Owner.Command.UpdateOwner;

public class UpdateOwnerCommandHandler(ILogger<UpdateOwnerCommandHandler> logger, IMapper mapper, IOwnerRepository ownerRepository) : IRequestHandler<UpdateOwnerCommand>
{
    public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update owner with Id: {@OwnerId} with {@UpdateOwner}", request.Id, request);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.Id);
        if (owner == null)
        {
            throw new NotFoundException(nameof(Owner), request.Id.ToString());
        }
        mapper.Map(request, owner);
        await ownerRepository.SaveChanges();
    }
}
