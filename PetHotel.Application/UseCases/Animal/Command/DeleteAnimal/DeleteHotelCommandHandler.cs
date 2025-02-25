using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Hotel.Command.DeleteHotel;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;
using System.ComponentModel;

namespace PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;

public class DeleteHotelCommandHandler(ILogger<DeleteHotelCommandHandler> logger, IAnimalRepository animalRepository) : IRequestHandler<DeleteHotelCommand>
{
    public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
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

