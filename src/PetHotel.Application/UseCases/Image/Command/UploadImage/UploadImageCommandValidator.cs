using FluentValidation;

namespace PetHotel.Application.UseCases.Image.Command.UploadImage;

public class UploadImageCommandValidator: AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {
        RuleFor(dto => dto.Description)
            .MaximumLength(500)
            .WithMessage("The description must contain a maximum of 500 characters.");
    }
}
