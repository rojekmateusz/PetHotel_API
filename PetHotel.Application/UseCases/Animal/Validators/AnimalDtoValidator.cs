using FluentValidation;
using PetHotel.Application.UseCases.Animal.Dto;

namespace PetHotel.Application.UseCases.Animal.Validators
{
    public class AnimalDtoValidator: AbstractValidator<AnimalDto>
    {
        public AnimalDtoValidator() 
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
