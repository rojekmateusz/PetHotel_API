﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Animal.Dto;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Animal.Queries.GetAllAnimals;

public class GetAllAnimalsQueryHandler(ILogger<GetAllAnimalsQueryHandler> logger, IMapper mapper, IAnimalRepository animalRepository) : IRequestHandler<GetAllAnimalsQuery, IEnumerable<AnimalDto>>
{
    public async Task<IEnumerable<AnimalDto>> Handle(GetAllAnimalsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all animals");
        var animals = await animalRepository.GetAllAnimalsAsync();
        var animalsDto = mapper.Map<IEnumerable<AnimalDto>>(animals);
        return animalsDto;
    }
}
