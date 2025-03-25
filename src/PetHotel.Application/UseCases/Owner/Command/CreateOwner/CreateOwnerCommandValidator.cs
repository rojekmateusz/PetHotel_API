using FluentValidation;

namespace PetHotel.Application.UseCases.Owner.Command.CreateOwner;

public class CreateOwnerCommandValidator : AbstractValidator<CreateOwnerCommand>
{
    public CreateOwnerCommandValidator()
    {
        RuleFor(dto => dto.FirstName)
           .Length(2, 20)
           .WithMessage("Hotel Name must contain from 3 to 20 characters");

        RuleFor(dto => dto.LastName)
           .Length(2, 20)
           .WithMessage("Hotel Name must contain from 3 to 20 characters");

        RuleFor(dto => dto.Email)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.PostalCode)
           .Matches(@"^\d{2}-\d{3}$")
           .WithMessage("Please provide a valid postal code (XX-XXX).");
    }
}
