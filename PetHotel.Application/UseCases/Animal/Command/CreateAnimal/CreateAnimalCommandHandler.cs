using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Animal.Command.CreateAnimal;

public class CreateAnimalCommandHandler(ILogger<CreateAnimalCommandHandler> logger, IMapper mapper, IAnimalRepository animalRepository, IOwnerRepository ownerRepository,
    IOwnerAuthorizationService ownerAuthorizationService) : IRequestHandler<CreateAnimalCommand, int>
{
    public async Task<int> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Animal {@Animal}", request);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerID)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerID.ToString());

        if (!ownerAuthorizationService.Authorize(owner, ResourceOperation.Create))
            throw new ForbidException();

        var animal = mapper.Map<Domain.Entities.Animal>(request);
        int id = await animalRepository.CreateAnimal(animal);
        return id;
    }
}
