using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Hotel.Command.CreateHotel;
using PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Hotel.Command.UpdateHotel;

public class UpdateHotelCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidatorErrors()
    {
        // arrange

        var command = new UpdateHotelCommand()
        {
            HotelName = "Dog Hotel",
            HotelsNIP = "1234567890",
            Email = "contact@doghotel.com",
            PostalCode = "12-345",
            IsActive = "Active"
        };

        var validator = new UpdateHotelCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidatorErrors()
    {
        // arrange

        var command = new UpdateHotelCommand()
        {
            HotelName = "D",
            HotelsNIP = "123",
            Email = "contactdoghotel.com",
            PostalCode = "12345",
            IsActive = "ready"
        };

        var validator = new UpdateHotelCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(r => r.HotelName);
        result.ShouldHaveValidationErrorFor(r => r.HotelsNIP);
        result.ShouldHaveValidationErrorFor(r => r.Email);
        result.ShouldHaveValidationErrorFor(r => r.PostalCode);
        result.ShouldHaveValidationErrorFor(r => r.IsActive);
    }


    [Theory()]
    [InlineData("Active")]
    [InlineData("Inactive")]

    public void Validator_ForValidIsActive_ShouldNotHaveValidationErrorsForIsActiveProperty(string status)
    {
        // arrange

        var validator = new UpdateHotelCommandValidator();

        var command = new UpdateHotelCommand { IsActive = status };

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

        var validator = new UpdateHotelCommandValidator();

        var command = new UpdateHotelCommand { PostalCode = postalCode };

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}