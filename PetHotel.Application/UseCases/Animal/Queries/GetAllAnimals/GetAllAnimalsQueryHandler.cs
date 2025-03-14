using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Animal.Dto;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Animal.Queries.GetAllAnimals;

public class GetAllAnimalsQueryHandler(ILogger<GetAllAnimalsQueryHandler> logger, IMapper mapper, IAnimalRepository animalRepository, 
    IOwnerRepository ownerRepository, IOwnerAuthorizationService ownerAuthorizationService) : IRequestHandler<GetAllAnimalsQuery, IEnumerable<AnimalDto>>
{
    public async Task<IEnumerable<AnimalDto>> Handle(GetAllAnimalsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all animals");
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerId.ToString());

        if (!ownerAuthorizationService.Authorize(owner, ResourceOperation.Read))
            throw new ForbidException();

        var animals = await animalRepository.GetAllAnimalsAsync();
        var animalsDto = mapper.Map<IEnumerable<AnimalDto>>(animals);
        return animalsDto;
    }
}
