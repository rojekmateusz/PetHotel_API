using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Service.Command.CreateService;
using PetHotel.Application.UseCases.Service.Command.UpdateService;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Service.Command.UpdateService;

public class UpdateServiceCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveAnyValidatorErrors()
    {
        // arrange

        var commad = new UpdateServiceCommand
        {
            Description = "Some valid description"
        };

        var validator = new UpdateServiceCommandValidator();

        // act

        var result = validator.TestValidate(commad);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidatorErrors()
    {
        // arrange

        var commad = new UpdateServiceCommand
        {
            Description = new string('a', 501)
        };

        var validator = new UpdateServiceCommandValidator();

        // act

        var result = validator.TestValidate(commad);

        // assert

        result.ShouldHaveValidationErrorFor(r => r.Description);
    }
}