using FluentValidation;

namespace PetHotel.Application.UseCases.Service.Command.UpdateService;

public class UpdateServiceCommandValidator: AbstractValidator<UpdateServiceCommand>
{
	public UpdateServiceCommandValidator()
	{
		RuleFor(dto => dto.Description)
            .MaximumLength(500)
            .WithMessage("The description must contain a maximum of 500 characters.");
    }
}
