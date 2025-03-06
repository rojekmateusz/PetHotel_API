using FluentValidation;

namespace PetHotel.Application.UseCases.Animal.Command.UpdateAnimal;

public class UpdateCommandValidator : AbstractValidator<UpdateAnimalCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(dto => dto.Name)
                .Length(2, 30)
                .WithMessage("The Name must contain from 2 to 30 characters"); ;

        RuleFor(dto => dto.Age)
            .GreaterThan(0)
            .WithMessage("Age must by greater than 0");

        RuleFor(dto => dto.Weight)
            .GreaterThan(0)
            .WithMessage("Weight must by greater than 0");
    }
}
