using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Payment.Command.UpdatePayment;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Payment.Command.UpdatePayment;

public class UpdatePaymentCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidatorErrors()
    {
        // arrange

        var command = new UpdatePaymentCommand
        {
            Amount = 100,
            Status = "Paid",
        };

        var validator = new UpdatePaymentCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidatorErrors()
    {
        // arrange

        var command = new UpdatePaymentCommand
        {
            Amount = -100,
            Status = "InvalidStatus",
        };

        var validator = new UpdatePaymentCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(x => x.Amount);
        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    [Theory()]
    [InlineData("paid")]
    [InlineData("cancelled")]
    [InlineData("unpaid")]
    public void Validator_ForInvalidStatus_ShouldNotHaveValidationErrorsForStatusProperty(string status)
    {
        // arrange

        var validator = new UpdatePaymentCommandValidator();

        var command = new UpdatePaymentCommand { Status = status };

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(x => x.Status);
    }
}