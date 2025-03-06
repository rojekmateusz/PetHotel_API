using FluentValidation;
using PetHotel.Application.UseCases.Animal.Dto;

namespace PetHotel.Application.UseCases.Animal.Command.CreateAnimal
{
    public class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
    {
        public CreateAnimalCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(2, 30);


            RuleFor(dto => dto.Age)
                .GreaterThan(0)
                .WithMessage("Age must by greater than 0");

            RuleFor(dto => dto.Weight)
                .GreaterThan(0)
                .WithMessage("Weight must by greater than 0");
        }
    }
}
