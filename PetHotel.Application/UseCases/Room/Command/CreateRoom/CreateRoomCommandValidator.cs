using FluentValidation;
using System.ComponentModel.Design;

namespace PetHotel.Application.UseCases.Room.Command.CreateRoom;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    private readonly List<string> validIsActive = ["Active", "Inactive"];
    public CreateRoomCommandValidator()
    {
        RuleFor(dto => dto.IsAvailable)
            .Must(validIsActive.Contains)
            .WithMessage("Invalid status.Please choose from the valid categories. Active or Inactive");
    }
}
