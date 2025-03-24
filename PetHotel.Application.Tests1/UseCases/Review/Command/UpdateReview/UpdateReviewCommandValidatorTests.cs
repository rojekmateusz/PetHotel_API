using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Review.Command.CreateReview;
using PetHotel.Application.UseCases.Review.Command.UpdateReview;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Review.Command.UpdateReview;

public class UpdateReviewCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveAnyValidatorErrors()
    {
        // arrange

        var command = new UpdateReviewCommand
        {
            Comment = "This is a valid comment"
        };

        var validator = new UpdateReviewCommandValidator();

        // act 

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShoulHaveValidatorErrors()
    {
        // arrange

        var command = new UpdateReviewCommand
        {
            Comment = new string('a', 501)
        };

        var validator = new UpdateReviewCommandValidator();

        // act 

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(r => r.Comment);
    }
}