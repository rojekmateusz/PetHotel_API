using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.Owner.Dto;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Owner.Command.CreateOwner;

public class CreateOwnerCommandHandler(ILogger<CreateOwnerCommandHandler> logger, IMapper mapper, IOwnerRepository ownerRepository) : IRequestHandler<CreateOwnerCommand, int>
{
    public async Task<int> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Owner {@Owner}", request);
        var owner = mapper.Map<Domain.Entities.Owner>(request);
        int id = await ownerRepository.CreateOwner(owner);
        return id;
    }
}
