using FluentValidation;
using System.ComponentModel.Design;

namespace PetHotel.Application.UseCases.Room.Command.CreateRoom;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    private readonly List<string> validIsAvailable = ["Available", "Unavailable"];
    public CreateRoomCommandValidator()
    {
        RuleFor(dto => dto.IsAvailable)
            .Must(validIsAvailable.Contains)
            .WithMessage("Invalid status.Please choose from the valid categories. Available or Unavailable");
    }
}
