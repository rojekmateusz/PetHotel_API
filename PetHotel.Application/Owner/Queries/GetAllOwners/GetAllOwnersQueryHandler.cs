using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.Owner.Dto;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Owner.Queries.GetAllOwners;

public class GetAllOwnersQueryHandler(ILogger<GetAllOwnersQueryHandler> logger, IMapper mapper, IOwnerRepository ownerRepository) : IRequestHandler<GetAllOwnersQuery, IEnumerable<OwnerDto>>
{
    public async Task<IEnumerable<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all owners");
        var owners = await ownerRepository.GetAllOwnersAsync();
        var ownersDto = mapper.Map<IEnumerable<OwnerDto>>(owners);
        return ownersDto;
    }
}
