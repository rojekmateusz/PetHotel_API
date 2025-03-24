using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Service.Command.CreateService;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Service.Command.CreateService;

public class CreateServiceCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveAnyValidatorErrors()
    {
        // arrange

        var commad = new CreateServiceCommand
        {
            Description = "Some valid description"  
        };

        var validator = new CreateServiceCommandValidator();

        // act

        var result = validator.TestValidate(commad);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidatorErrors()
    {
        // arrange

        var commad = new CreateServiceCommand
        {
            Description = new string('a', 501)
        };

        var validator = new CreateServiceCommandValidator();

        // act

        var result = validator.TestValidate(commad);

        // assert

        result.ShouldHaveValidationErrorFor(r => r.Description);
    }
}