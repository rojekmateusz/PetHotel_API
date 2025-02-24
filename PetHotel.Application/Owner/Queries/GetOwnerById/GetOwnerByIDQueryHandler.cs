using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.Owner.Dto;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Owner.Queries.GetOwnerById;

public class GetOwnerByIDQueryHandler(ILogger<GetOwnerByIDQueryHandler> logger, IMapper mapper, IOwnerRepository ownerRepository) : IRequestHandler<GetOwnerByIDQuery, OwnerDto>
{
    public async Task<OwnerDto> Handle(GetOwnerByIDQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting owner by Id: {Id}", request.Id);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.Id);
        var ownerDto = mapper.Map<OwnerDto>(owner);
        return ownerDto;
    }
}
