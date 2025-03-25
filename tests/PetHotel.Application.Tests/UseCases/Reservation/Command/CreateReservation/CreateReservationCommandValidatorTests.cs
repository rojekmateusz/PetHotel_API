using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Reservation.Command.CreateReservation;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Reservation.Command.CreateReservation;

public class CreateReservationCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveAnyCommandErrors()
    {
        // arrange

        var command = new CreateReservationCommand
        {
            Notes = "This is a valid note"
        };

        var validator = new CreateReservationCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveCommandErrors()
    {
        // arrange

        var validator = new CreateReservationCommandValidator();

        var command = new CreateReservationCommand
        {
            Notes = new string('a', 501)
        };
        
        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(r => r.Notes);
    }
}