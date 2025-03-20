using FluentValidation;

namespace PetHotel.Application.UseCases.Reservation.Command.CreateReservation;

public class CreateReservationCommandValidator: AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(dto => dto.Notes)
            .MaximumLength(500)
            .WithMessage("Notes must not exceed 500 characters.");
    }
}
