using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Owner.Dto;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Owner.Queries.GetOwnerById;

public class GetOwnerByIDQueryHandler(ILogger<GetOwnerByIDQueryHandler> logger, IMapper mapper, IOwnerRepository ownerRepository,
     IOwnerAuthorizationService ownerAuthorizationService) : IRequestHandler<GetOwnerByIDQuery, OwnerDto>
{
    public async Task<OwnerDto> Handle(GetOwnerByIDQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting owner by Id: {Id}", request.Id);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Owner), request.Id.ToString());

        if(!ownerAuthorizationService.Authorize(owner, ResourceOperation.Read))
            throw new ForbidException();

        var ownerDto = mapper.Map<OwnerDto>(owner);
        return ownerDto;
    }
}
