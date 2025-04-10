﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Animal.Command.UpdateAnimal;

public class UpdateAniamlCommandHandler(ILogger<UpdateAniamlCommandHandler> logger, IMapper mapper, IAnimalRepository animalRepository, IOwnerRepository ownerRepository,
    IOwnerAuthorizationService ownerAuthorizationService) : IRequestHandler<UpdateAnimalCommand>
{
    public async Task Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Animal with Id: {@AnimalId} with {@UpdateAnimal}", request.Id, request);

        var owner = await ownerRepository.GetOwnerByIdAsync(request.OwnerID)
            ?? throw new NotFoundException(nameof(Owner), request.OwnerID.ToString());

        if (!ownerAuthorizationService.Authorize(owner, ResourceOperation.Update))
            throw new ForbidException();

        var animal = await animalRepository.GetAnimalByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Animal), request.Id.ToString());

        mapper.Map(request, animal);
        await animalRepository.SaveChanges();
    }
}
