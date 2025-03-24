using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Hotel.Command.CreateHotel;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Hotel.Command.CreateHotel;

public class CreateHotelValidatorTests
{
    [Fact()]
    public void Validato_ForValidCommand_ShouldNotHaveValidatorErrors()
    {
        // arrange

        var command = new CreateHotelCommand()
        {
            HotelName = "Dog Hotel",
            HotelsNIP = "1234567890",
            Email = "contact@doghotel.com",
            PostalCode = "12-345",
            IsActive = "Active"
        };

        var validator = new CreateHotelCommandValidator();

        // act 

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
    {
        var command = new CreateHotelCommand()
        {
            HotelName = "D",
            HotelsNIP = "123456789",
            Email = "@doghotel.com",
            PostalCode = "12345",
            IsActive = "ready"
        };

        var validator = new CreateHotelCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(a => a.HotelName);
        result.ShouldHaveValidationErrorFor(a => a.HotelsNIP);
        result.ShouldHaveValidationErrorFor(a => a.Email);
        result.ShouldHaveValidationErrorFor(a => a.PostalCode);
        result.ShouldHaveValidationErrorFor(a => a.IsActive);
    }

    [Theory()]
    [InlineData("Active")]
    [InlineData("Inactive")]
   
    public void Validator_ForValidIsActive_ShouldNotHaveValidationErrorsForIsActiveProperty(string status)
    {
        // arrange

        var validator = new CreateHotelCommandValidator();

        var command = new CreateHotelCommand { IsActive = status };

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveValidationErrorFor(c => c.IsActive);

    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // arrange

        var validator = new CreateHotelCommandValidator();

        var command = new CreateHotelCommand { PostalCode = postalCode };

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}