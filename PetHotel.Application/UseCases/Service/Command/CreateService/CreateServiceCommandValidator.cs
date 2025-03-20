using FluentValidation;

namespace PetHotel.Application.UseCases.Service.Command.CreateService;

public class CreateServiceCommandValidator: AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(dto => dto.Description)
            .MaximumLength(500)
            .WithMessage("The description must contain a maximum of 500 characters.");
    }
}
