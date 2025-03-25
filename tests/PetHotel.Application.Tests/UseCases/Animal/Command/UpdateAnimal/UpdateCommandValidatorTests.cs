using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Animal.Command.UpdateAnimal;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Animal.Command.UpdateAnimal;

public class UpdateCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidatorErrors()
    {
        // arrange

        var command = new UpdateAnimalCommand()
        {
            Name = "Pusia",
            Age = 1,
            Weight = 3,
        };

        var validator = new UpdateCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
    {
        // arrange

        var command = new UpdateAnimalCommand()
        {
            Name = "P",
            Age = 0,
            Weight = 0
        };

        var validator = new UpdateCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(a => a.Name);
        result.ShouldHaveValidationErrorFor(a => a.Age);
        result.ShouldHaveValidationErrorFor(a => a.Weight);
    }
}