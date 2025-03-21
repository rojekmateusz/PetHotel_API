using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Animal.Command.CreateAnimal;

public class CreateAnimalCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateAnimalCommand()
        {
            Name = "Pusia",
            Age = 1,
            Weight = 3,
        };

        var validator = new CreateAnimalCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();

    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateAnimalCommand()
        {
            Name = "P",
            Age = 0,
            Weight = 0
        };

        var validator = new CreateAnimalCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(a => a.Name);
        result.ShouldHaveValidationErrorFor(a => a.Age);
        result.ShouldHaveValidationErrorFor(a => a.Weight);


    }
}