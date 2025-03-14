using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Animal.Dto;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Animal.Queries.GetAnimalById;

public class GetAnimalByIdQueryHandler(ILogger<GetAnimalByIdQueryHandler> logger, IMapper mapper, IAnimalRepository animalRepository,
    IOwnerRepository ownerRepository, IOwnerAuthorizationService ownerAuthorizationService) : IRequestHandler<GetAnimalByIdQuery, AnimalDto>
{
    public async Task<AnimalDto> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting animal by Id: {Id}", request.Id);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.Id.ToString());

        if (!ownerAuthorizationService.Authorize(owner, ResourceOperation.Read))
            throw new ForbidException();

        var animal = await animalRepository.GetAnimalByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Animal), request.Id.ToString());

        var animalDto = mapper.Map<AnimalDto>(animal);
        return animalDto;
    }
}
