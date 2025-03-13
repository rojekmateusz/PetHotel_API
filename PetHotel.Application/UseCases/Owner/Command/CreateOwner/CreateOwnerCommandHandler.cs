using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.User;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Owner.Command.CreateOwner;

public class CreateOwnerCommandHandler(ILogger<CreateOwnerCommandHandler> logger, IMapper mapper, IOwnerRepository ownerRepository,
    IUserContext userContext) : 
    IRequestHandler<CreateOwnerCommand, int>
{
    public async Task<int> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Owner {@Owner}", request);
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{UserEmail} [{UserId}] is creating a new owner {@Owner}", currentUser.Email, currentUser.Id, request);
        
        var owner = mapper.Map<Domain.Entities.Owner>(request);
        owner.UserId = currentUser.Id;  
        int id = await ownerRepository.CreateOwner(owner);
        return id;
    }
}
