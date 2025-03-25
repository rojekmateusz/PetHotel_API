using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Review.Command.CreateReview;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Review.Command.CreateReview;

public class CreateReviewCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveAnyValidatorErrors()
    {
        // arrange

        var command = new CreateReviewCommand
        {
            Comment = "This is a valid comment"
        };

        var validator = new CreateReviewCommandValidator();

        // act 

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShoulHaveValidatorErrors()
    {
        // arrange

        var command = new CreateReviewCommand
        {
            Comment = new string('a', 501)
        };

        var validator = new CreateReviewCommandValidator();

        // act 

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(r => r.Comment);
    }
}