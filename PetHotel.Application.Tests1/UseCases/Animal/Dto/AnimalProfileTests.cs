using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Animal.Command.UpdateAnimal;
using PetHotel.Application.UseCases.Animal.Dto;
using System.Runtime.CompilerServices;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Animal.Dto;

public class AnimalProfileTests
{
    private IMapper _mapper;

    public AnimalProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AnimalProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_FromAnimalToAnimalDto_MapsCorrectly()
    {
        // arrange

        var animal = new Domain.Entities.Animal
        {
            Id = 1,
            Name = "Name Test",
            Spiecies = "Spiecies Test",
            Breed = "Breed Test",
            Age = 1,
            Note = "Note Test",
            Weight = 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            OwnerID = 1,
        };

        // act

        var animalDto = _mapper.Map<AnimalDto>(animal);

        // assert

        animalDto.Should().NotBeNull();
        animalDto.Id.Should().Be(animal.Id);
        animalDto.Name.Should().Be(animal.Name);
        animalDto.Spiecies.Should().Be(animal.Spiecies);
        animalDto.Breed.Should().Be(animal.Breed);
        animalDto.Age.Should().Be(animal.Age);
        animalDto.Note.Should().Be(animal.Note);
        animalDto.Weight.Should().Be(animal.Weight);
        animalDto.CreatedAt.Should().Be(animal.CreatedAt);
        animalDto.UpdatedAt.Should().Be(animal.UpdatedAt);
        animalDto.OwnerID.Should().Be(animal.OwnerID);
    }

    [Fact()]
    public void CreateMap_FromUpdateAnimalToAnimal_MapsCorrectly()
    {
        // arrange

        var command = new UpdateAnimalCommand
        {
            Id = 1,
            Name = "Name Test",
            Spiecies = "Spiecies Test",
            Breed = "Breed Test",
            Age = 1,
            Note = "Note Test",
            Weight = 1,
            UpdatedAt = DateTime.Now,
            OwnerID = 1,
        };

        // act

        var animal = _mapper.Map<Domain.Entities.Animal>(command);

        // assert

        animal.Should().NotBeNull();
        animal.Id.Should().Be(animal.Id);
        animal.Name.Should().Be(animal.Name);
        animal.Spiecies.Should().Be(animal.Spiecies);
        animal.Breed.Should().Be(animal.Breed);
        animal.Age.Should().Be(animal.Age);
        animal.Note.Should().Be(animal.Note);
        animal.Weight.Should().Be(animal.Weight);
        animal.UpdatedAt.Should().Be(animal.UpdatedAt);
        animal.OwnerID.Should().Be(animal.OwnerID);
    }

    [Fact()]
    public void CreateMap_FromCreateAnimalToAnimal_MapsCorrectly()
    {
        // arrange

        var command = new CreateAnimalCommand
        {
            Name = "Name Test",
            Spiecies = "Spiecies Test",
            Breed = "Breed Test",
            Age = 1,
            Note = "Note Test",
            Weight = 1,
            CreatedAt = DateTime.Now,
            OwnerID = 1,
        };

        // act

        var animal = _mapper.Map<Domain.Entities.Animal>(command);

        // assert

        animal.Should().NotBeNull();
        animal.Id.Should().Be(animal.Id);
        animal.Name.Should().Be(animal.Name);
        animal.Spiecies.Should().Be(animal.Spiecies);
        animal.Breed.Should().Be(animal.Breed);
        animal.Age.Should().Be(animal.Age);
        animal.Note.Should().Be(animal.Note);
        animal.Weight.Should().Be(animal.Weight);
        animal.UpdatedAt.Should().Be(animal.UpdatedAt);
        animal.OwnerID.Should().Be(animal.OwnerID);
    }
}