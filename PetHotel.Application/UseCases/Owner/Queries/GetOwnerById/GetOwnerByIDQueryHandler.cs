using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Owner.Dto;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Owner.Queries.GetOwnerById;

public class GetOwnerByIDQueryHandler(ILogger<GetOwnerByIDQueryHandler> logger, IMapper mapper, IOwnerRepository ownerRepository) : IRequestHandler<GetOwnerByIDQuery, OwnerDto>
{
    public async Task<OwnerDto> Handle(GetOwnerByIDQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting owner by Id: {Id}", request.Id);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Owner), request.Id.ToString());
        var ownerDto = mapper.Map<OwnerDto>(owner);
        return ownerDto;
    }
}
