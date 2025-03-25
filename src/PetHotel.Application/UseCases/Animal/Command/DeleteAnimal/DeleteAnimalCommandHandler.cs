using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;

public class DeleteAnimalCommandHandler(ILogger<DeleteAnimalCommandHandler> logger, IAnimalRepository animalRepository, IOwnerRepository ownerRepository,
    IOwnerAuthorizationService ownerAuthorizationService) : IRequestHandler<DeleteAnimalCommand>
{
    public async Task Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting animal with id {Id}", request.Id);
        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerId)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerId.ToString());

        if (!ownerAuthorizationService.Authorize(owner, ResourceOperation.Delete))
            throw new ForbidException();

        var animal = await animalRepository.GetAnimalByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Animal), request.Id.ToString());

        await animalRepository.DeleteAnimal(animal);
    }
}

