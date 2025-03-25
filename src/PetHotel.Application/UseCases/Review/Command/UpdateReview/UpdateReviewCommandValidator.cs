using FluentValidation;

namespace PetHotel.Application.UseCases.Review.Command.UpdateReview;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(dto => dto.Comment)
           .MaximumLength(500)
           .WithMessage("The comment must contain a maximum of 500 characters.");
    }
}
