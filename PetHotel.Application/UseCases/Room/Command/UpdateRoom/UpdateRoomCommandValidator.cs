using FluentValidation;

namespace PetHotel.Application.UseCases.Room.Command.UpdateRoom;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    private readonly List<string> validIsActive = ["Active", "Inactive"];
    public UpdateRoomCommandValidator()
    {
        RuleFor(dto => dto.IsAvailable)
            .Must(validIsActive.Contains)
            .WithMessage("Invalid status.Please choose from the valid categories.Active or Inactive");
    }
}
