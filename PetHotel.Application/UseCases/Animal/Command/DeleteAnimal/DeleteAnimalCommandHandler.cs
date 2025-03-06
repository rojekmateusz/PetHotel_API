using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;

public class DeleteAnimalCommandHandler(ILogger<DeleteAnimalCommandHandler> logger, IAnimalRepository animalRepository) : IRequestHandler<DeleteAnimalCommand>
{
    public async Task Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting animal with id {Id}", request.Id);
        var animal = await animalRepository.GetAnimalByIdAsync(request.Id);
        if (animal == null)
        {
            throw new NotFoundException(nameof(animal), request.Id.ToString());
        }
        await animalRepository.DeleteAnimal(animal);
    }
}

