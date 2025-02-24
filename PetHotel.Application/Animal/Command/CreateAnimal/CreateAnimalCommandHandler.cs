using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.Animal.Command.CreateAnimal;

public class CreateAnimalCommandHandler(ILogger<CreateAnimalCommandHandler> logger, IMapper mapper, IAnimalRepository animalRepository) : IRequestHandler<CreateAnimalCommand, int>
{
    public async Task<int> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Animal {@Animal}",request);
        var animal = mapper.Map<Domain.Entities.Animal>(request);
        int id = await animalRepository.CreateAnimal(animal);
        return id;
    }
}
