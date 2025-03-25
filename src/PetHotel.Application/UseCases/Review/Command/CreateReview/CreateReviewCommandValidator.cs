using FluentValidation;

namespace PetHotel.Application.UseCases.Review.Command.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(dto => dto.Comment)
            .MaximumLength(500)
            .WithMessage("The comment must contain a maximum of 500 characters.");
    }
}
