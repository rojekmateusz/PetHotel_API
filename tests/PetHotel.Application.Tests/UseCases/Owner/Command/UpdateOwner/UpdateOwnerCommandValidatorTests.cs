using Xunit;
using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Owner.Command.UpdateOwner;
using PetHotel.Application.UseCases.Hotel.Command.CreateHotel;
using PetHotel.Application.UseCases.Owner.Command.CreateOwner;

namespace PetHotel.Application.Tests.UseCases.Owner.Command.UpdateOwner;
public class UpdateOwnerCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrorsForCommandErrors()
    {
        // arrange

        var command = new UpdateOwnerCommand()
        {
            FirstName = "John",
            LastName = "Kowalski",
            Email = "jakkowalksi@gmail.com",
            PostalCode = "12-345"
        };

        var validator = new UpdateOwnerCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrorsForCommandErrors()
    { 
        // arrange
        
        var command = new UpdateOwnerCommand()
        {
            FirstName = "J",
            LastName = "K",
            Email = "jakkowalksigmail.com",
            PostalCode = "12345"
        };

        var validator = new UpdateOwnerCommandValidator();

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

        var validator = new UpdateOwnerCommandValidator();

        var command = new UpdateOwnerCommand { PostalCode = postalCode };

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}