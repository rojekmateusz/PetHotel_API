using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Owner.Command.CreateOwner;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Owner.Command.CreateOwner;

public class CreateOwnerCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrorForCommandErrors()
    {
        // arrange

        var command = new CreateOwnerCommand()
        {
            FirstName = "John",
            LastName = "Kowalski",
            Email = "jakkowalksi@gmail.com",
            PostalCode = "12-345"
        };

        var validator = new CreateOwnerCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidationErrorForCommandErrors()
    {
        // arrange

        var command = new CreateOwnerCommand()
        {
            FirstName = "J",
            LastName = "K",
            Email = "jakkowalksigmail.com",
            PostalCode = "12345"
        };

        var validator = new CreateOwnerCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert


        result.ShouldHaveValidationErrorFor(a => a.FirstName);
        result.ShouldHaveValidationErrorFor(a => a.LastName);
        result.ShouldHaveValidationErrorFor(a => a.Email);
        result.ShouldHaveValidationErrorFor(a => a.PostalCode);
    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // arrange

        var validator = new CreateOwnerCommandValidator();

        var command = new CreateOwnerCommand { PostalCode = postalCode };

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}